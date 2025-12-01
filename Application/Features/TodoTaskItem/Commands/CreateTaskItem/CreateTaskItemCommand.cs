using MediatR;

namespace Application.Features.TodoTaskItem.Commands.CreateTaskItem;

public record CreateTaskItemCommand(
    string Title,
    string Description,
    int UserId
) : IRequest<CreateTaskItemResponse>;
