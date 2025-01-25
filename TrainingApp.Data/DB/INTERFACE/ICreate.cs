namespace TrainingAppData.DB.INTERFACE
{
    public interface ICreate<T>
    {
        Task Create(T entity);
    }
}
