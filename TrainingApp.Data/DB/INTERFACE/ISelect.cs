namespace TrainingAppData.DB.INTERFACE
{
    public interface ISelect<T>
    {
        T Select(Guid guid);
        T Select(int id);
    }
}
