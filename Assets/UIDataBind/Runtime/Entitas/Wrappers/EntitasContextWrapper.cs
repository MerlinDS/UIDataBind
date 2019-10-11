using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Utils.Extensions;

namespace UIDataBind.Entitas.Wrappers
{
    public struct EntitasContextWrapper : IProperties
    {
        private readonly UiBindContext _context;
        public string ModelPath { get; }

        public EntitasContextWrapper(IContext<UiBindEntity> context, string modelPath)
        {
            _context = (UiBindContext) context;
            ModelPath = modelPath;
        }


        public TEntity GetPropertyEntity<TEntity>(string propertyName, bool createIfNull = false)
            where TEntity : class => _context.GetEntity<TEntity>(ModelPath.BuildPath(propertyName), createIfNull);
    }
}