﻿namespace SipayData.SeedWork;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}
