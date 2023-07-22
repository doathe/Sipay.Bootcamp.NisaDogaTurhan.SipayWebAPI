namespace SipayData.Extensions;

public static class ExtensionClass
{
    // Converts DateOnly format to DateTime
    public static DateTime ConvertDateTime(DateOnly dateOnly)
    {
        return new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day);
    }
}
