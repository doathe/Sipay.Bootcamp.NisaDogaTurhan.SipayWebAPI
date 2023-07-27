namespace SipayWebAPI.Models.ViewModels.Users;

public class UpdateUserModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordVerify { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Phone { get; set; }
    public int LicenseNumber { get; set; }
}
