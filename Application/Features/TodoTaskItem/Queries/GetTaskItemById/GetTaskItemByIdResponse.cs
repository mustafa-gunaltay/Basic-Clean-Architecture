namespace Application.Features.TodoTaskItem.Queries.GetTaskItemById;

public record GetTaskItemByIdResponse(
    int Id,
    string Title,
    string Description,
    bool IsCompleted,
    DateTime CreatedAt,
    int UserId
);
