using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Role;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;


namespace Application.Usecases.V1.Role.Queries;
public sealed class GetRoleQueryHandler : IQueryHandler<Query.GetRoleQuery, PagedResult<Responses.RoleResponse>>
{
    private readonly RoleManager<AppRole> roleManager;
    private readonly IMapper mapper;

    public GetRoleQueryHandler(
        RoleManager<AppRole> roleManager,
        IMapper mapper)
    {
        this.roleManager = roleManager;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.RoleResponse>>> Handle(Query.GetRoleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Check value search is nullOrWhiteSpace?
            var EventsQuery = GetRolesQuery(request.SearchTerm); // If Not GetAll With Name Or Address Contain searchTerm

            // GetList by Pagination
            var Events = await PagedResult<AppRole>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.RoleResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private IQueryable<AppRole> GetRolesQuery(string? searchValue)
    {
        if (string.IsNullOrWhiteSpace(searchValue))
        {
            return roleManager.Roles;
        }

        return roleManager.Roles.Where(x => x.Name.Contains(searchValue));
    }
}
