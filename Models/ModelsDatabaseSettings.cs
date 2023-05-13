namespace ArMapAdmin.Models
{
    public class ModelsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ModelsCollectionName { get; set; } = null!;
    }
}
