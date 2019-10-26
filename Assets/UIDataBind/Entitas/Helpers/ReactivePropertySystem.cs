using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Entitas.Helpers
{
    public abstract class ReactivePropertySystem<TViewModel> : ReactiveSystem<UiBindEntity>
        where TViewModel : struct, IViewModel
    {
        private readonly UiBindContext _context;

        protected ReactivePropertySystem(UiBindContext context, BindingPath propertyPath)
            : this(context, typeof(TViewModel).Name, propertyPath)
        {
        }

        protected ReactivePropertySystem(UiBindContext context, BindingPath modelPath, BindingPath propertyPath)
            : base(context)
        {
            _context = context;
            ModelPath = modelPath;
            PropertyPath = BindingPath.BuildFrom(modelPath, propertyPath);
        }

        protected BindingPath PropertyPath { get; }

        protected BindingPath ModelPath { get; }

        protected sealed override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.AllOf(UiBindMatcher.Property, UiBindMatcher.ModelPath,
                                                        UiBindMatcher.Dirty));

        protected sealed override bool Filter(UiBindEntity entity) =>
            entity.IsModel(PropertyPath);

        protected sealed override void Execute(List<UiBindEntity> entities)
        {
            var model = _context.GetModel<TViewModel>(ModelPath);
            Execute(ref model);
            _context.Fetch(ModelPath, model);

        }

        protected abstract void Execute(ref TViewModel model);
    }
}