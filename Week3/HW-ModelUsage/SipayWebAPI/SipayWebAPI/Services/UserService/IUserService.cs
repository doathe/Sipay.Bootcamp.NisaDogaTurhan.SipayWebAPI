using SipayData.Entities;
using SipayWebAPI.Models.ViewModels.Users;
using SipayWebAPI.Services.BaseService;

namespace SipayWebAPI.Services.UserService;

public interface IUserService
{
    public Task<IEnumerable<UserViewModel>> GetAllAsync();
    public Task<UserViewModel> GetByIdAsync(int id);

    public Task CreateAsync(CreateUserModel entity);
    public Task UpdateAsync(int id, UpdateUserModel entity);
    public Task DeleteByIdAsync(int id);

    public Task UpdateUserPasswordAsync(int id, string password);
    public Task<string> UserLoginAsync(string email, string password);
}
