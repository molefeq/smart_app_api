namespace SmartData.Data.ViewModels
{
    public class ReferenceDataModel
    {
        public ReferenceDataModel() { }

        public ReferenceDataModel(long id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }

        public ReferenceDataModel(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
