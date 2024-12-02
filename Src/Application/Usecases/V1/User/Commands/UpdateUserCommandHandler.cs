using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Services.V1.User;
using Domain.Entities.Identity;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace Application.Usecases.V1.User.Commands;
public sealed class UpdateUserCommandHandler : ICommandHandler<Command.UpdateUserCommand, Responses.UserGetByIdResponse>
{
    private readonly UserManager<AppUser> userManager;
    private readonly IMapper mapper;
    private readonly IBlobStorageService blobStorageService;
    public UpdateUserCommandHandler(
        UserManager<AppUser> userManager,
        IMapper mapper,
        IBlobStorageService blobStorageService)
    {
        this.userManager = userManager;
        this.mapper = mapper;
        this.blobStorageService = blobStorageService;
    }
    public async Task<Result<Responses.UserGetByIdResponse>> Handle(Command.UpdateUserCommand request, CancellationToken cancellationToken)
    {
        string newBlobPath = string.Empty;
        string oldBlobPath = string.Empty;

        try
        {
            // Check Id exist
            var user = await userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
                return Result.Failure<Responses.UserGetByIdResponse>("Không tìm thấy người dùng.");

            // Upload new image if provided
            oldBlobPath = user.Avatar;
            newBlobPath = await UploadNewAvatarAsync(request, user.Email);

            // Update User
            user.Update(request, newBlobPath);
            await userManager.UpdateAsync(user);
            var response = mapper.Map<Responses.UserGetByIdResponse>(user);


            // Delete oldBlobPath
            if (!string.IsNullOrEmpty(newBlobPath))
                await DeleteOldAvatarAsync(oldBlobPath);

            return Result.Success(response, 202);
        }
        catch (Exception ex)
        {
            if (string.IsNullOrEmpty(newBlobPath))
                await blobStorageService.DeleteBlobSnapshotsAsync(newBlobPath);
            throw new Exception(ex.Message);
        }
    }
    private async Task DeleteOldAvatarAsync(string oldBlobPath)
    {
        if (!string.IsNullOrEmpty(oldBlobPath))
            await blobStorageService.DeleteBlobAsync(oldBlobPath);
    }

    private async Task<string> UploadNewAvatarAsync(Command.UpdateUserCommand request, string email)
    {
        if (request.Avatar == null)
            return string.Empty;

        string[] parts = email.Split('@');
        var nameImage = $"{parts[0]}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

        return await blobStorageService.UploadBlob(
            image: request.Avatar,
            name: nameImage,
            folder: BlobFolder.avatars);
    }
}
