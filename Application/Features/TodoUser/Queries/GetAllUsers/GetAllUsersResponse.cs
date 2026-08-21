namespace Application.Features.TodoUser.Queries.GetAllUsers;

public record GetAllUsersResponse(
    int Id,
    string Username,
    string Email,
    int TaskCount
);
