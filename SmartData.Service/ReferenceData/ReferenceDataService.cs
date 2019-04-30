using SmartData.Data.ViewModels;
using SmartData.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace SmartData.Service.ReferenceData
{
    public class ReferenceDataService : IReferenceDataService
    {
        private IUnitOfWork unitOfWork;

        public ReferenceDataService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public StaticDataModel GetStaticData()
        {
            return new StaticDataModel
            {
                Countries = GetCountries()
            };
        }

        public List<ReferenceDataModel> GetCountries()
        {
            var countries = unitOfWork.Country.GetEntities();

            return countries.Select(c => new ReferenceDataModel(c.Id, c.Name, c.Code)).ToList();
        }
    }
}
