namespace Application.Features.TodoUser.Queries.GetUserById;

public record GetUserByIdResponse(
    int Id,
    string Username,
    string Email,
    int TaskCount
);
