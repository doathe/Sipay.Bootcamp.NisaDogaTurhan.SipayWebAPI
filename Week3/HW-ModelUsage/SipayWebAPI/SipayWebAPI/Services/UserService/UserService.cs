using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SipayData.Context;
using SipayData.Entities;
using SipayWebAPI.Models.ViewModels.Users;
using SipayWebAPI.Services.BaseService;
using System.Xml.Linq;

namespace SipayWebAPI.Services.UserService;

public class UserService : IUserService
{
    private readonly SipayDbContext _context;
    public UserService(SipayDbContext context)
    {
        _context = context;
    }

    // GetAll method returns all data from the requested table.
    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        List<User> users = await _context.Set<User>().ToListAsync();
        List<UserViewModel> viewModels = new List<UserViewModel>();
        foreach (var item in users)
        {
            viewModels.Add(new UserViewModel()
            {
                FullName = item.FirstName + " " + item.LastName,
                Email = item.Email,
                BirthDate = item.BirthDate,
                Phone = item.Phone,
                LicenseNumber = item.LicenseNumber
            });
        }

        return viewModels;
    }

    // GetById method returns data from the requested table with id data.
    public async Task<UserViewModel> GetByIdAsync(int id)
    {
        var user = await _context.Set<User>().FindAsync(id);
        UserViewModel viewModel = new UserViewModel
        {
            FullName = user.FirstName + " " + user.LastName,
            Email = user.Email,
            BirthDate = user.BirthDate,
            Phone = user.Phone,
            LicenseNumber = user.LicenseNumber,
        };

        return viewModel;
    }

    // Create method saves the entered data in the correct table.
    public async Task CreateAsync(CreateUserModel entity)
    {
        User user = new User
        {
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Password = entity.Password,
            PasswordVerify = entity.PasswordVerify,
            BirthDate = entity.BirthDate,
            Phone = entity.Phone,
            LicenseNumber = entity.LicenseNumber
        };
        user.CreatedOn = DateTime.UtcNow;
        user.UpdatedOn = DateTime.UtcNow;

        var existCheck = _context.Set<User>().Any(i => i.Email == user.Email);
        if (existCheck)
        {
            throw new InvalidOperationException("User already exist");
        }

        await _context.Set<User>().AddAsync(user);
        await _context.SaveChangesAsync();
    }

    // Delete method, deletes the entered data from the correct table with id data.
    public async Task DeleteByIdAsync(int id)
    {
        var entity = await _context.Set<User>().FirstOrDefaultAsync(i => i.Id == id);
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _context.Set<User>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    // Update method, updates the entered data from the correct table.
    public async Task UpdateAsync(int id, UpdateUserModel entity)
    {
        var existingEntity = _context.Set<User>().FirstOrDefault(i => i.Id == id);
        if (existingEntity == null)
            throw new ArgumentNullException(nameof(entity));

        User user = new User
        {
            Id = id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Password = entity.Password,
            PasswordVerify = entity.PasswordVerify,
            BirthDate = entity.BirthDate,
            Phone = entity.Phone,
            LicenseNumber = entity.LicenseNumber
        };
        user.UpdatedOn = DateTime.UtcNow;

        _context.Entry(existingEntity).CurrentValues.SetValues(user);
        await _context.SaveChangesAsync();
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
