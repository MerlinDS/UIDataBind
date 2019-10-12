namespace UIDataBind.Base
{
    public interface IProperties
    {
        string ModelPath { get; }
        IEntityManager EntityManager { get; }
        TEntity GetPropertyEntity<TEntity>(string propertyName, bool createIfNull = false) where TEntity : class;
    }
}