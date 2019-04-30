using SmartData.DataAccess.Models;
using SmartData.DataAccess.Repositories;
using System.Threading.Tasks;

namespace SmartData.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private SmartAppContext context;

        #region Private Repositories Fields

        private GenericRepository<Country> country;
        private GenericRepository<Role> role;
        private GenericRepository<Account> account;
        private GenericRepository<DeviceDetail> deviceDetail;
        private GenericRepository<DeviceStatus> deviceStatus;

        #endregion

        public UnitOfWork(SmartAppContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #region Repositories Properties

        public GenericRepository<Country> Country
        {
            get
            {
                if (country == null)
                {
                    country = new GenericRepository<Country>(context);
                }

                return country;
            }
        }

        public GenericRepository<Role> Role
        {
            get
            {
                if (role == null)
                {
                    role = new GenericRepository<Role>(context);
                }

                return role;
            }
        }

        public GenericRepository<Account> Account
        {
            get
            {
                if (account == null)
                {
                    account = new GenericRepository<Account>(context);
                }

                return account;
            }

        }

        public GenericRepository<DeviceDetail> DeviceDetail
        {
            get
            {
                if (deviceDetail == null)
                {
                    deviceDetail = new GenericRepository<DeviceDetail>(context);
                }

                return deviceDetail;
            }
        }

        public GenericRepository<DeviceStatus> DeviceStatus
        {
            get
            {
                if (deviceStatus == null)
                {
                    deviceStatus = new GenericRepository<DeviceStatus>(context);
                }

                return deviceStatus;
            }
        }

        #endregion
    }
}
