using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Role;
using Domain.Abstractions;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Application.Usecases.V1.Role.Commands;
public sealed class CreateRoleCommandHandler : ICommandHandler<Command.CreateRoleCommand, Responses.RoleResponse>
{
    private readonly RoleManager<AppRole> roleManager;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateRoleCommandHandler(
        RoleManager<AppRole> roleManager,
        IMapper mapper,
        IUnitOfWork unitOfWork)

    {
        this.roleManager = roleManager;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Responses.RoleResponse>> Handle(Command.CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByNameAsync(request.Name);

        if (role != null)
            return Result.Failure<Responses.RoleResponse>("Tên Role đã tồn tại.", StatusCodes.Status400BadRequest);

        var newRole = new AppRole
        {
            Name = request.Name,
            Description = request.Description,
        };


        var createRoleResult = await roleManager.CreateAsync(newRole);
        if (!createRoleResult.Succeeded)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var error in createRoleResult.Errors)
                stringBuilder.Append(error.Description + ",");

            return Result.Failure<Responses.RoleResponse>(stringBuilder.ToString());
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        var resultResponse = mapper.Map<Responses.RoleResponse>(newRole);
        return Result.Success(resultResponse, 201);

    }
}
