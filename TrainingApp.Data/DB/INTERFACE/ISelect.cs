namespace TrainingAppData.DB.INTERFACE
{
    public interface ISelect<T>
    {
        //starting with C# 8.0, you can define static methods in interfaces, and static methods must have a body
        public Task<T> Select(Guid guid);
        public Task<T> Select(int id);
        public Task<List<T>> SelectList();
    }
}
