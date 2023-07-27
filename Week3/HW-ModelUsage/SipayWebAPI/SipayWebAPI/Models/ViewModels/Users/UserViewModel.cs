namespace SipayWebAPI.Models.ViewModels.Users;

public class UserViewModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Phone { get; set; }
    public int LicenseNumber { get; set; }
}
