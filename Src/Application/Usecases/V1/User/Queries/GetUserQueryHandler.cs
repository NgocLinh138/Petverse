using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.User;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Usecases.V1.User.Queries;
public sealed class GetUserQueryHandler : IQueryHandler<Query.GetUserQuery, PagedResult<Responses.UserGetAllResponse>>
{
    private readonly UserManager<AppUser> userManager;
    private readonly IMapper mapper;
    public GetUserQueryHandler(
        UserManager<AppUser> userManager,
        IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }
    public async Task<Result<PagedResult<Responses.UserGetAllResponse>>> Handle(Query.GetUserQuery request, CancellationToken cancellationToken)
    {
        IQueryable<AppUser> EventsQuery;

        EventsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
            ? userManager.Users.Where(x => x.IsDeleted == false)
            : userManager.Users.Where(x => x.UserName.Contains(request.SearchTerm) && x.IsDeleted == false);

        EventsQuery = request.RoleId == null
            ? EventsQuery
            : EventsQuery.Where(x => x.RoleId == request.RoleId);

        var Events = await PagedResult<AppUser>.CreateAsync(EventsQuery,
            request.PageIndex,
            request.PageSize);

        var result = mapper.Map<PagedResult<Responses.UserGetAllResponse>>(Events);


        return Result.Success(result);
    }
}
