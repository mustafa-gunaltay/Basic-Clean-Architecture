namespace Application.Features.TodoTaskItem.Commands.DeleteTaskItem;

public record DeleteTaskItemResponse(
    bool IsSuccess,
    string Message
);
