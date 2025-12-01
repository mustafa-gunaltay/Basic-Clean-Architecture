using MediatR;

namespace Application.Features.TodoTaskItem.Commands.DeleteTaskItem;

public record DeleteTaskItemCommand(int Id) : IRequest<DeleteTaskItemResponse>;
