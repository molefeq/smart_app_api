using System.Collections.Generic;

namespace SmartData.Data.ViewModels
{
    public class StaticDataModel
    {
        public List<ReferenceDataModel> Provinces { get; set; }
        public List<ReferenceDataModel> Countries { get; set; }
        public List<ReferenceDataModel> AccountTypes { get; set; }
        public List<ReferenceDataModel> Languages { get; set; }
        public List<ReferenceDataModel> MaritalStatuses { get; set; }
        public List<ReferenceDataModel> EthnicGroups { get; set; }
        public List<ReferenceDataModel> Titles { get; set; }
    }
}
