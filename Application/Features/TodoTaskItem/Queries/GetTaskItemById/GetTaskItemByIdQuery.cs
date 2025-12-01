using MediatR;

namespace Application.Features.TodoTaskItem.Queries.GetTaskItemById;

public record GetTaskItemByIdQuery(int Id) : IRequest<GetTaskItemByIdResponse?>;
