namespace TrainingAppData.DB.INTERFACE
{
    public interface ISelect<T>
    {
        //starting with C# 8.0, you can define static methods in interfaces, and static methods must have a body
        static T Select(Guid guid)
        {
            throw new NotImplementedException();
        }
        static T Select(int id)
        {
            throw new NotImplementedException();
        }
        static List<T> SelectList()
        {
            throw new NotImplementedException();
        }
    }
}
