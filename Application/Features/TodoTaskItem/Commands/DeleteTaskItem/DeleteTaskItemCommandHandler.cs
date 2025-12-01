using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Features.TodoTaskItem.Commands.DeleteTaskItem;

public class DeleteTaskItemCommandHandler : IRequestHandler<DeleteTaskItemCommand, DeleteTaskItemResponse>
{
    private readonly ITaskItemRepository _taskItemRepository;

    public DeleteTaskItemCommandHandler(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public async Task<DeleteTaskItemResponse> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = _taskItemRepository.Delete(request.Id);

        if (isDeleted)
        {
            return new DeleteTaskItemResponse(true, $"TaskItem with Id {request.Id} deleted successfully.");
        }

        return new DeleteTaskItemResponse(false, $"TaskItem with Id {request.Id} not found.");
    }
}
