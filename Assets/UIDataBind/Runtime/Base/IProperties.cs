namespace UIDataBind.Base
{
    public interface IProperties
    {
        string ModelPath { get; }
        TEntity GetPropertyEntity<TEntity>(string propertyName, bool createIfNull = false) where TEntity : class;
    }
}