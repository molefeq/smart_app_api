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
        private GenericRepository<Tier> tier;
        private GenericRepository<PaymentDetail> paymentDetail;
        private GenericRepository<ExchangeRate> exchangeRate;
        private GenericRepository<TopupOption> topupOption;
        private GenericRepository<Currency> currency;

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

        public GenericRepository<Tier> Tier
        {
            get
            {
                if (tier == null)
                {
                    tier = new GenericRepository<Tier>(context);
                }

                return tier;
            }
        }

        public GenericRepository<PaymentDetail> PaymentDetail
        {
            get
            {
                if (paymentDetail == null)
                {
                    paymentDetail = new GenericRepository<PaymentDetail>(context);
                }

                return paymentDetail;
            }
        }

        public GenericRepository<ExchangeRate> ExchangeRate
        {
            get
            {
                if (exchangeRate == null)
                {
                    exchangeRate = new GenericRepository<ExchangeRate>(context);
                }

                return exchangeRate;
            }
        }

        public GenericRepository<TopupOption> TopupOption
        {
            get
            {
                if (topupOption == null)
                {
                    topupOption = new GenericRepository<TopupOption>(context);
                }

                return topupOption;
            }
        }

        public GenericRepository<Currency> Currency
        {
            get
            {
                if (currency == null)
                {
                    currency = new GenericRepository<Currency>(context);
                }

                return currency;
            }
        }

        #endregion
    }
}
