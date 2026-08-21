namespace Application.Features.TodoUser.Commands.CreateUser;

public record CreateUserResponse(
    int Id,
    string Username,
    string Email,
    int TaskCount
);
