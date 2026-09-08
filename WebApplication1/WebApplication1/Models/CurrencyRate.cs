namespace WebApplication1.Models;

public class CurrencyRate
{
    public string CurrencyCode { get; set; } = string.Empty;
    public string CurrencyName { get; set; } = string.Empty;
    public double CurrencyRateValue { get; set; }
}