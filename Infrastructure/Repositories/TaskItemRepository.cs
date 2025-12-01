using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository // adapter
    {
        private readonly ApplicationDbContext _context;

        public TaskItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public TaskItem Create(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            _context.SaveChanges();
            return taskItem;
        }

        public TaskItem? GetById(int id)
        {
            return _context.TaskItems
                .Include(t => t.User) // User navigation property'yi yükle
                .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _context.TaskItems
                .Include(t => t.User) // User navigation property'yi yükle
                .ToList();
        }

        public TaskItem Update(TaskItem taskItem)
        {
            _context.TaskItems.Update(taskItem);
            _context.SaveChanges();
            return taskItem;
        }

        public bool Delete(int id)
        {
            var taskItem = _context.TaskItems.FirstOrDefault(t => t.Id == id);
            if (taskItem == null)
                return false;

            _context.TaskItems.Remove(taskItem);
            _context.SaveChanges();
            return true;
        }
    }
}
