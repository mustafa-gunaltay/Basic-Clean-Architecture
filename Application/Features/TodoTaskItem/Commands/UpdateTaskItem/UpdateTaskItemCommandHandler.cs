using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.TodoTaskItem.Commands.UpdateTaskItem;

public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand, UpdateTaskItemResponse>
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IUserRepository _userRepository;

    public UpdateTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IUserRepository userRepository)
    {
        _taskItemRepository = taskItemRepository;
        _userRepository = userRepository;
    }

    public async Task<UpdateTaskItemResponse> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
    {
        // Önce mevcut task'ı bul
        var existingTask = _taskItemRepository.GetById(request.Id);
        
        if (existingTask == null)
        {
            throw new KeyNotFoundException($"TaskItem with Id {request.Id} not found.");
        }

        // UserId'nin geçerli olup olmadığını kontrol et
        var userExists = _userRepository.GetById(request.UserId);
        if (userExists == null)
        {
            throw new KeyNotFoundException($"User with Id '{request.UserId}' not found. Please provide a valid UserId.");
        }

        // Güncelle
        existingTask.Title = request.Title;
        existingTask.Description = request.Description;
        existingTask.IsCompleted = request.IsCompleted;
        existingTask.UserId = request.UserId;

        var updatedTaskItem = _taskItemRepository.Update(existingTask);

        return new UpdateTaskItemResponse(
            updatedTaskItem.Id,
            updatedTaskItem.Title,
            updatedTaskItem.Description ?? string.Empty,
            updatedTaskItem.IsCompleted,
            updatedTaskItem.CreatedAt,
            updatedTaskItem.UserId
        );
    }
}
