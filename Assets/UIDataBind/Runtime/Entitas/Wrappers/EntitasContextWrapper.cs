using System;
using Entitas;
using UIDataBind.Base;

namespace UIDataBind.Entitas.Wrappers
{
    public struct EntitasContextWrapper : IProperties
    {
        private readonly string _modelPath;
        private readonly UiBindContext _context;

        public EntitasContextWrapper(IContext<UiBindEntity> context, string modelPath)
        {
            _modelPath = modelPath;
            _context = (UiBindContext) context;
        }

        public void UpdateModel<TViewModel>(ref TViewModel model) where TViewModel : IViewModel =>
            model.Update(this);

        void IProperties.Fetch<TViewModel>(TViewModel model) => model.Fetch(this);

        public TEntity GetPropertyEntity<TEntity>(string propertyName, bool createIfNull = false)
            where TEntity : class
        {
            var propertyPath = _modelPath + propertyName;
            //TODO: Convert path to guid
            var guid = Guid.Empty;

            var entity = _context.GetEntityWithViewModel(guid);
            if (!createIfNull || entity != null)
                return entity as TEntity;

            entity = _context.CreateEntity();
            entity.AddViewModel(guid);
            return entity as TEntity;
        }
    }
}