namespace TrainingAppData.DB.INTERFACE
{
    public interface IDelete<T>
    {
        void Delete(Guid guid);
        void Delete(int id, DateTime deleteDate);
    }
}
