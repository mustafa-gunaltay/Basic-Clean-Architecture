namespace Application.Features.TodoTaskItem.Commands.CreateTaskItem;

public record CreateTaskItemResponse(
    int Id,
    string Title,
    string Description,
    bool IsCompleted,
    DateTime CreatedAt,
    int UserId
);
