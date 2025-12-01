namespace Application.Features.TodoTaskItem.Queries.GetAllTaskItems;

public record GetAllTaskItemsResponse(
    int Id,
    string Title,
    string Description,
    bool IsCompleted,
    DateTime CreatedAt,
    int UserId
);
