namespace HseAr.DataAccess.Mongodb
{
    public class ModelsDatabaseSettings : IModelsDatabaseSettings
    { 
        public string ScenesCollectionName { get; set; }
        public string ModelsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ModificationsCollectionName { get; set; }
    }

    public interface IModelsDatabaseSettings
    {
        string ScenesCollectionName { get; set; }
        string ModelsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ModificationsCollectionName { get; set; }
    }
}