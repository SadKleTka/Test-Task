using BitcoinApi.Entities;
using BitcoinApi.Entities.DTO;
public interface WorkerService
{
   public void AddToDatabase(Bitcoin bitcoin);

   public BitcoinToResponse GetLastBitcoin();
}