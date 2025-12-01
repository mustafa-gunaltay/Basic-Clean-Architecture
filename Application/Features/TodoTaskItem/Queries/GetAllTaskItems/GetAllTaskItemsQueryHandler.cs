using Application.Interfaces;
using MediatR;

namespace Application.Features.TodoTaskItem.Queries.GetAllTaskItems;

public class GetAllTaskItemsQueryHandler : IRequestHandler<GetAllTaskItemsQuery, IEnumerable<GetAllTaskItemsResponse>>
{
    private readonly ITaskItemRepository _taskItemRepository;

    public GetAllTaskItemsQueryHandler(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public async Task<IEnumerable<GetAllTaskItemsResponse>> Handle(GetAllTaskItemsQuery request, CancellationToken cancellationToken)
    {
        var taskItems = _taskItemRepository.GetAll();

        return taskItems.Select(taskItem => new GetAllTaskItemsResponse(
            taskItem.Id,
            taskItem.Title,
            taskItem.Description ?? string.Empty,
            taskItem.IsCompleted,
            taskItem.CreatedAt,
            taskItem.UserId
        ));
    }
}
