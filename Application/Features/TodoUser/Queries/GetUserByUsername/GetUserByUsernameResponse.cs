namespace Application.Features.TodoUser.Queries.GetUserByUsername;

public record GetUserByUsernameResponse(
    int Id,
    string Username,
    string Email,
    int TaskCount
);
