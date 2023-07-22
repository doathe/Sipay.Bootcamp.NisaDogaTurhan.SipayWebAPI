using SipayData.Context;
using SipayData.Entities;
using SipayWebAPI.Services.BaseService;

namespace SipayWebAPI.Services.RentalService;

public class RentalService : GenericService<Rental>, IRentalService
{
    public RentalService(SipayDbContext context) : base(context)
    {
    }
}
