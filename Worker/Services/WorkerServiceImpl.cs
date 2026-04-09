using BitcoinApi.Entities;
using BitcoinApi.Context;
using BitcoinApi.Entities.DTO;
public class WorkerServiceImpl : WorkerService
{
    private readonly AppDbContext _dbContext;

    public WorkerServiceImpl(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddToDatabase(Bitcoin bitcoin)
    {
        if (bitcoin is null)
        {
            throw new KeyNotFoundException("Bitcoin data is null");
        }

        _dbContext.Bitcoins.Add(bitcoin);
        _dbContext.SaveChanges();
    }

    public BitcoinToResponse GetLastBitcoin()
    {
        var lastBitcoin = _dbContext.Bitcoins.OrderByDescending(b => b.Timestamp).FirstOrDefault();

        if (lastBitcoin is null)
        {
            throw new KeyNotFoundException("No Bitcoin data found in the database");
        }

        return MapToBitcoinToResponse(lastBitcoin);
    }

    private BitcoinToResponse MapToBitcoinToResponse(Bitcoin bitcoin)
    {
        return new BitcoinToResponse(bitcoin.Price, bitcoin.Timestamp);
    }
}