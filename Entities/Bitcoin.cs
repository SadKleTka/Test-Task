namespace BitcoinApi.Entities;
public class Bitcoin
{
    public long Id { get; set;}
    public decimal Price { get; set; }
    public string Currency { get; set;} = "USD";
    public DateTime Timestamp { get; set; }

    public Bitcoin(decimal price, DateTime timestamp)
    {
        Price = price;
        Timestamp = timestamp;
    }
}