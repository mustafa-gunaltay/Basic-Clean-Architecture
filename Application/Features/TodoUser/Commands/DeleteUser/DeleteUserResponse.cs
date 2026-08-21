namespace Application.Features.TodoUser.Commands.DeleteUser;

public record DeleteUserResponse(
    bool IsSuccess,
    string Message
);
