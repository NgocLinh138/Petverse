using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Enumerations;
using Contract.Services.V1.Application;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Usecases.V1.Application.Commands;

public sealed class UpdateApplicationStatusCommandHandler : ICommandHandler<Command.UpdateApplicationStatusCommand, Responses.ApplicationResponse>
{
    private readonly IApplicationRepository applicationRepository;
    private readonly IPetCenterRepository petCenterRepository;
    private readonly IPetCenterServiceRepository petCenterServiceRepository;
    private readonly UserManager<AppUser> userManager;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public UpdateApplicationStatusCommandHandler(
        IApplicationRepository applicationRepository,
        IPetCenterRepository petCenterRepository,
        IPetCenterServiceRepository petCenterServiceRepository,
        UserManager<AppUser> userManager,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        this.applicationRepository = applicationRepository;
        this.petCenterRepository = petCenterRepository;
        this.petCenterServiceRepository = petCenterServiceRepository;
        this.userManager = userManager;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Responses.ApplicationResponse>> Handle(Command.UpdateApplicationStatusCommand request, CancellationToken cancellationToken)
    {
        var application = await applicationRepository.FindByIdAsync(request.Id, cancellationToken);
        if (application == null)
            return Result.Failure<Responses.ApplicationResponse>("Không tìm thấy đơn ứng tuyển.", StatusCodes.Status404NotFound);

        var user = await userManager.FindByIdAsync(application.UserId.ToString());
        if (user == null || user.IsDeleted)
            return Result.Failure<Responses.ApplicationResponse>("Không tìm thấy người dùng.", StatusCodes.Status404NotFound);

        if ((application.Status == JobApplicationStatus.Approve || application.Status == JobApplicationStatus.Cancel)
               && request.Status == JobApplicationStatus.Processing)
        {
            return Result.Failure<Responses.ApplicationResponse>("Không thể thay đổi trạng thái thành Đang xử lý.");
        }

        if ((application.Status == JobApplicationStatus.Processing || application.Status == JobApplicationStatus.Cancel)
            && request.Status == JobApplicationStatus.Approve)
        {
            if (user.RoleId != request.RoleId)
            {
                await ApproveApplication(application, request);
            }
        }

        UpdateApplicationStatus(application, request);

        applicationRepository.Update(application);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<Responses.ApplicationResponse>(application);
        return Result.Success(response, 202);
    }

    private async Task<Result<Responses.ApplicationResponse>> ApproveApplication(Domain.Entities.Application application, Command.UpdateApplicationStatusCommand request)
    {
        var user = await userManager.FindByIdAsync(application.UserId.ToString());
        if (user == null)
            return Result.Failure<Responses.ApplicationResponse>("Không tìm thấy người dùng.", StatusCodes.Status404NotFound);

        await UpdateUserRole(user, request.RoleId);

        return await CreatePetCenter(application, request);
    }

    private async Task UpdateUserRole(AppUser user, Guid newRoleId)
    {
        user.RoleId = newRoleId;
        var updateResult = await userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Không cập nhật được role của người dùng: {errors}");
        }
    }

    private async Task<Result<Responses.ApplicationResponse>> CreatePetCenter(Domain.Entities.Application application, Command.UpdateApplicationStatusCommand request)
    {
        var petCenter = new Domain.Entities.PetCenter
        {
            ApplicationId = application.Id,
            NumPet = 0,
            IsVerified = request.IsVerified
        };

        await petCenterRepository.AddAsync(petCenter);

        foreach (var applicationPetService in application.ApplicationPetServices)
        {
            var petCenterService = new Domain.Entities.PetCenterService
            {
                PetCenterId = petCenter.Id,
                PetServiceId = applicationPetService.PetServiceId,
                Price = 0,
                Rate = 0,
                Description = application.Description,
                Type = ServiceType.Fixed
            };

            await petCenterServiceRepository.AddAsync(petCenterService);
        }

        return Result.Success<Responses.ApplicationResponse>(201);
    }

    private void UpdateApplicationStatus(Domain.Entities.Application application, Command.UpdateApplicationStatusCommand request)
    {
        application.Status = request.Status;
        application.UpdatedDate = TimeZones.GetSoutheastAsiaTime();

        if (request.Status == JobApplicationStatus.Cancel && !string.IsNullOrEmpty(request.CancelReason))
        {
            application.CancelReason = request.CancelReason;
        }
    }
}
