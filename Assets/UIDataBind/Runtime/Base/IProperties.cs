namespace UIDataBind.Base
{
    public interface IProperties
    {
        BindingPath ModelPath { get; }
        IEntityManager EntityManager { get; }
        TEntity GetPropertyEntity<TEntity>(BindingPath propertyName, bool createIfNull = false) where TEntity : class;
    }
}