using MediatR;

namespace Application.Features.TodoTaskItem.Queries.GetAllTaskItems;

public record GetAllTaskItemsQuery() : IRequest<IEnumerable<GetAllTaskItemsResponse>>;
