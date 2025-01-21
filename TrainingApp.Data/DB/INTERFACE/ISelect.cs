namespace TrainingAppData.DB.INTERFACE
{
    public interface ISelect<T>
    {
        //starting with C# 8.0, you can define static methods in interfaces, and static methods must have a body
        public T Select(Guid guid);
        public T Select(int id);
        public List<T> SelectList();
    }
}
