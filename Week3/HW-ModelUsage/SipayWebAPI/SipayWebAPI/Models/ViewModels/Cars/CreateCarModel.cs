namespace SipayWebAPI.Models.ViewModels.Cars;

public class CreateCarModel
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public int Year { get; set; }
    public string FuelType { get; set; }

    public int UserId { get; set; }
}
