namespace UIDataBind.Base
{
    public interface IEntityProvider
    {
        IUiBindEntity Entity { get; }
        IEntityManager EntityManager { get; }
    }
}