using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Role;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Application.Usecases.V1.Role.Queries;
public sealed class GetRoleByIdQueryHandler : IQueryHandler<Query.GetRoleByIdQuery, Responses.RoleResponse>
{
    private readonly RoleManager<AppRole> roleManager;
    private readonly IMapper mapper;

    public GetRoleByIdQueryHandler(
         RoleManager<AppRole> roleManager,
        IMapper mapper)
    {
        this.roleManager = roleManager;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.RoleResponse>> Handle(Query.GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await roleManager.FindByIdAsync(request.Id.ToString());
        if (result == null)
            return Result.Failure<Responses.RoleResponse>("Không tìm thấy role.", StatusCodes.Status404NotFound);

        var resultResponse = mapper.Map<Responses.RoleResponse>(result);
        return Result.Success(resultResponse);
    }
}
