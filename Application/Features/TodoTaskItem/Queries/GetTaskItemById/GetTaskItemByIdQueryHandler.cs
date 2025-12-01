using Application.Interfaces;
using MediatR;

namespace Application.Features.TodoTaskItem.Queries.GetTaskItemById;

public class GetTaskItemByIdQueryHandler : IRequestHandler<GetTaskItemByIdQuery, GetTaskItemByIdResponse?>
{
    private readonly ITaskItemRepository _taskItemRepository;

    public GetTaskItemByIdQueryHandler(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public async Task<GetTaskItemByIdResponse?> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
    {
        var taskItem = _taskItemRepository.GetById(request.Id);

        if (taskItem == null)
            return null;

        return new GetTaskItemByIdResponse(
            taskItem.Id,
            taskItem.Title,
            taskItem.Description ?? string.Empty,
            taskItem.IsCompleted,
            taskItem.CreatedAt,
            taskItem.UserId
        );
    }
}
