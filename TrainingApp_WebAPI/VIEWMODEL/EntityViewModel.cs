namespace TrainingApp_WebAPI.VIEWMODEL
{
    public abstract class EntityViewModel
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int VersionId { get; set; }
    }
}
