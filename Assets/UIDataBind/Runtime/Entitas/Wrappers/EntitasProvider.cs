using UIDataBind.Base;

namespace UIDataBind.Entitas.Wrappers
{
    public class EntitasProvider : IEntityProvider
    {
        public IUiBindEntity Entity { get; }
        public IEntityManager EntityManager { get; }

        public EntitasProvider(IUiBindEntity entity, IEntityManager entityManager)
        {
            Entity = entity;
            EntityManager = entityManager;
        }
    }
}