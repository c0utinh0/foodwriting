using FoodWriting.Domain.Entities;

namespace FoodWriting.Domain.Interfaces;

public interface IUserRepository
{
    Task Create(User user);
    Task<bool> FindUserByEmail(string email);
}
