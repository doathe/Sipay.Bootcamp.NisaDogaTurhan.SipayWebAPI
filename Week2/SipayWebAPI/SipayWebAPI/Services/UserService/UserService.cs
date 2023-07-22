using Microsoft.AspNetCore.Mvc;
using SipayData.Context;
using SipayData.Entities;
using SipayWebAPI.Services.BaseService;

namespace SipayWebAPI.Services.UserService;

public class UserService : GenericService<User>, IUserService
{
    private readonly SipayDbContext _context;
    public UserService(SipayDbContext context) : base(context)
    {
        _context = context;
    }

    // Patch method for password update
    public async Task UpdateUserPasswordAsync(int id, string password)
    {
        var existingEntity = _context.Users.FirstOrDefault(i => i.Id == id);
        if (existingEntity == null)
            throw new InvalidOperationException("User not found.");

        existingEntity.UpdatedOn = DateTime.Now;
        _context.Entry(existingEntity).CurrentValues.SetValues(existingEntity);
    }

    // Login method, it checks the entered values are match.
    public async Task<string> UserLoginAsync(string email, string password)
    {
        var entityCheck = _context.Users.Any(i => i.Email == email && i.Password == password);
        if (!entityCheck)
            throw new InvalidOperationException("User not found.");
        return "Successful Login";
    }
}
