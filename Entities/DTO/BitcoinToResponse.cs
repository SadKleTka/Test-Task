namespace BitcoinApi.Entities.DTO;
public class BitcoinToResponse
{
    public decimal Price { get; set; }

    public string currency { get; set; } = "USD";
    public DateTime Timestamp { get; set; }

    public BitcoinToResponse(decimal price, DateTime timestamp)
    {
        Price = price;
        Timestamp = timestamp;
    }
}