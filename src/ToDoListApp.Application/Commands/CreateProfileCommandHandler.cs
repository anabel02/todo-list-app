using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Domain;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class CreateProfileCommandHandler(ToDoContext context, ICurrentUser currentUser) : ICommandHandler<CreateProfileCommand, ProfileDto>
{
    public async Task<CommandResult<ProfileDto>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(currentUser.UserId))
            return CommandResult<ProfileDto>.Fail(HttpStatusCode.Unauthorized, "User is not authorized.");

        var exists = await context.Profiles
            .AnyAsync(p => p.UserId == currentUser.UserId, cancellationToken);

        if (exists)
        {
            return CommandResult<ProfileDto>.Fail(
                HttpStatusCode.Conflict,
                "Profile already exists for user."
            );
        }

        var profile = new Profile { UserId = currentUser.UserId };

        context.Profiles.Add(profile);
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult<ProfileDto>.Ok(profile.MapTo<ProfileDto>());
    }
}