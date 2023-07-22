using SipayData.Entities;
using SipayWebAPI.Services.BaseService;

namespace SipayWebAPI.Services.UserService;

public interface IUserService : IGenericService<User>
{
    public Task UpdateUserPasswordAsync(int id, string password);
    public Task<string> UserLoginAsync(string email, string password);
}
