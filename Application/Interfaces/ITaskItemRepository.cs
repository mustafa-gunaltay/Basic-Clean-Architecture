using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITaskItemRepository // port
    {
        TaskItem? GetById(int id);
        IEnumerable<TaskItem> GetAll();
        TaskItem Create(TaskItem taskItem);
        TaskItem Update(TaskItem taskItem);
        bool Delete(int id);
    }
}
