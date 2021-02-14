namespace HseAr.DataAccess.Mongodb
{ 
    public class ModelsDatabaseSettings 
    { 
        public string ScenesCollectionName { get; set; }
        
        public string ModelsCollectionName { get; set; }
        
        public string ConnectionString { get; set; }
        
        public string DatabaseName { get; set; }
        
        public string ModificationsCollectionName { get; set; }
    }
}