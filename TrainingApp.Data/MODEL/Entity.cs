namespace TrainingAppData.MODEL
{
    public class Entity
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
