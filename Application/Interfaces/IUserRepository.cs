using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository // port
    {
        User? GetById(int id);
        User? GetByUsername(string username);
        IEnumerable<User> GetAll();
        User Create(User user);
        User Update(User user);
        bool Delete(int id);
    }
}
