using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Role;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


namespace Application.Usecases.V1.Role.Commands;
public sealed class UpdateRoleCommandHandler : ICommandHandler<Command.UpdateRoleCommand, Responses.RoleResponse>
{
    private readonly RoleManager<AppRole> roleManager;
    private readonly IMapper mapper;

    public UpdateRoleCommandHandler(
        RoleManager<AppRole> roleManager
        , IMapper mapper)
    {
        this.roleManager = roleManager;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.RoleResponse>> Handle(Command.UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id.ToString());
        if (role == null)
            return Result.Failure<Responses.RoleResponse>("Không tìm thấy role.", StatusCodes.Status404NotFound);

        role.Name = request.Name.IsNullOrEmpty() ? role.Name : request.Name;
        role.Description = request.Description.IsNullOrEmpty() ? role.Description : request.Description;
        await roleManager.UpdateAsync(role);

        var resultResponse = mapper.Map<Responses.RoleResponse>(role);
        return Result.Success(resultResponse, 202);
    }
}
