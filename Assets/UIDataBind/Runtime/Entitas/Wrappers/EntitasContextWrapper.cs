using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Utils.Extensions;

namespace UIDataBind.Entitas.Wrappers
{
    public struct EntitasContextWrapper : IProperties
    {
        private readonly UiBindContext _context;
        public BindingPath ModelPath { get; }

        public EntitasContextWrapper(IContext<UiBindEntity> context, BindingPath modelPath)
        {
            _context = (UiBindContext) context;
            ModelPath = modelPath;
        }

        public IEntityManager EntityManager => _context.EntityManager;

        public TEntity GetPropertyEntity<TEntity>(BindingPath propertyName, bool createIfNull = false)
            where TEntity : class => _context.GetEntity<TEntity>(ModelPath.BuildPath(propertyName), createIfNull);
    }
}