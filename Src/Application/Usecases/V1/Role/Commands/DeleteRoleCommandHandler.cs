using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Role;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;


namespace Application.Usecases.V1.Role.Commands;
public sealed class DeleteRoleCommandHandler : ICommandHandler<Command.DeleteRoleCommand>
{
    private readonly RoleManager<AppRole> roleManager;
    public DeleteRoleCommandHandler(RoleManager<AppRole> roleManager)
    {
        this.roleManager = roleManager;
    }

    public async Task<Result> Handle(Command.DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id.ToString());
        if (role == null)
            return Result.Failure<Result>("Không tìm thấy role.", 404);


        await roleManager.DeleteAsync(role);
        return Result.Success(202);

    }
}
