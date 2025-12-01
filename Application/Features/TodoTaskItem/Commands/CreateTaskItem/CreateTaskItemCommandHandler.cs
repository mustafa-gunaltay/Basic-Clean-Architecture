using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.TodoTaskItem.Commands.CreateTaskItem;

public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand, CreateTaskItemResponse>
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IUserRepository _userRepository;

    public CreateTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IUserRepository userRepository)
    {
        _taskItemRepository = taskItemRepository;
        _userRepository = userRepository;
    }

    public async Task<CreateTaskItemResponse> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
    {
        // UserId'nin geçerli olup olmadığını kontrol et
        var userExists = _userRepository.GetById(request.UserId);
        if (userExists == null)
        {
            throw new KeyNotFoundException($"User with Id '{request.UserId}' not found. Please provide a valid UserId.");
        }

        var taskItem = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            UserId = request.UserId,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        var createdTaskItem = _taskItemRepository.Create(taskItem);

        return new CreateTaskItemResponse(
            createdTaskItem.Id,
            createdTaskItem.Title,
            createdTaskItem.Description ?? string.Empty,
            createdTaskItem.IsCompleted,
            createdTaskItem.CreatedAt,
            createdTaskItem.UserId
        );
    }
}
