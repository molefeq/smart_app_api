using SmartData.DataAccess.Models;
using SmartData.DataAccess.Repositories;
using System.Threading.Tasks;

namespace SmartData.DataAccess
{
    public interface IUnitOfWork
    {
        #region Repositories Properties

        GenericRepository<Account> Account { get; }
        GenericRepository<Country> Country { get; }
        GenericRepository<Role> Role { get; }
        GenericRepository<DeviceDetail> DeviceDetail { get; }
        GenericRepository<Tier> Tier { get; }
        GenericRepository<PaymentDetail> PaymentDetail { get; }
        GenericRepository<TopupOption> TopupOption { get; }
        GenericRepository<ExchangeRate> ExchangeRate { get; }
        GenericRepository<Currency> Currency { get; }
        #endregion

        Task SaveAsync();
        void Save();
    }
}
