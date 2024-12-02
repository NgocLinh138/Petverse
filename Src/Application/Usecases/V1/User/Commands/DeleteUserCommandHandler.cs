using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Services.V1;
using Contract.Services.V1.User;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Usecases.V1.User.Commands;
public sealed class DeleteUserCommandHandler : ICommandHandler<Command.DeleteUserCommand>
{
    private readonly UserManager<AppUser> userManager;
    private readonly RoleManager<AppRole> roleManager;
    private readonly IRedisCacheService redisCacheService;
    public DeleteUserCommandHandler(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IRedisCacheService redisCacheService)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.redisCacheService = redisCacheService;
    }
    public async Task<Result> Handle(Command.DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
            return Result.Failure("Không tìm thấy người dùng.", StatusCodes.Status404NotFound);

        // Can not delete Admin
        var role = await roleManager.FindByIdAsync(user.RoleId.ToString());
        if (role.Name.Equals("admin", StringComparison.InvariantCultureIgnoreCase))
            return Result.Failure("Không thể xóa Admin.", StatusCodes.Status400BadRequest);


        await MarkUserAsDeletedAsync(user);

        await CacheDeletedUserAsync(user);

        return Result.Success(202);
    }

    private async Task MarkUserAsDeletedAsync(AppUser user)
    {
        user.DeletedDate = TimeZones.GetSoutheastAsiaTime();
        user.IsDeleted = true;
        await userManager.UpdateAsync(user);
    }

    private async Task CacheDeletedUserAsync(AppUser user)
    {
        var key = $"{user.Id}";
        var expiryTime = TimeSpan.FromDays(7); // Time to expire the cache
        await redisCacheService.SetAsync(key, "true", null);
    }
}
