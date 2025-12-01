namespace Application.Features.TodoTaskItem.Commands.UpdateTaskItem;

public record UpdateTaskItemResponse(
    int Id,
    string Title,
    string Description,
    bool IsCompleted,
    DateTime CreatedAt,
    int UserId
);
