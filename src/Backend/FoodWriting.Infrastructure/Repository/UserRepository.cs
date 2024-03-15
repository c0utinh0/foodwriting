using FoodWriting.Domain.Entities;
using FoodWriting.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodWriting.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    
    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task Create(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<bool> FindUserByEmail(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email.Equals(email));
    }
}