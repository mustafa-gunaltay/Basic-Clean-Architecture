namespace Application.Features.TodoUser.Commands.UpdateUser;

public record UpdateUserResponse(
    int Id,
    string Username,
    string Email,
    int TaskCount
);
