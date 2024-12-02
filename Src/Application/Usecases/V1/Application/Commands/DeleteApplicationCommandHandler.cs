using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Application;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Application.Commands;

public sealed class DeleteApplicationCommandHandler : ICommandHandler<Command.DeleteApplicationCommand>
{
    private readonly IApplicationRepository applicationRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteApplicationCommandHandler(
        IApplicationRepository applicationRepository,
        IUnitOfWork unitOfWork)
    {
        this.applicationRepository = applicationRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await applicationRepository.FindByIdAsync(request.Id, cancellationToken);
        if (application == null)
            return Result.Failure("Không tìm thấy đơn ứng tuyển.", StatusCodes.Status404NotFound);

        var relatedPetServices = application.ApplicationPetServices.ToList();
        application.ApplicationPetServices.Clear();

        applicationRepository.Remove(application);
        await unitOfWork.SaveChangesAsync();

        return Result.Success(202);
    }
}
