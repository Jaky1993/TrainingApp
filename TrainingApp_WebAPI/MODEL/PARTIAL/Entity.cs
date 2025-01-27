namespace TrainingApp_WebAPI.MODEL
{
    public abstract partial class Entity<T>
    {
        public abstract List<Tuple<string, string>> DataValidation(T entity);
    }
}
