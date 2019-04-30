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
        GenericRepository<DeviceStatus> DeviceStatus { get; }

        #endregion

        Task SaveAsync();
        void Save();
    }
}
