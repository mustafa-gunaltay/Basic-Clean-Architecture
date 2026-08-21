using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository // adapter
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? GetById(int id)
        {
            return _context.Users
                .Include(u => u.TaskItems) // Navigation property'yi yükle
                .FirstOrDefault(u => u.Id == id);
        }

        public User? GetByUsername(string username)
        {
            return _context.Users
                .Include(u => u.TaskItems)
                .FirstOrDefault(u => u.Username == username);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(u => u.TaskItems) // Navigation property'yi yükle
                .ToList();
        }

        public User Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            // TaskItems yuklenmezse EF sadece User icin DELETE gonderir ve
            // FK_TaskItem_User kisitina takilir; Include ile cascade devreye girer
            var user = _context.Users
                .Include(u => u.TaskItems)
                .FirstOrDefault(u => u.Id == id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}
