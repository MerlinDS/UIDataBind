using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Entitas.Helpers
{
    public abstract class InitModelSystem<TViewModel> : ReactiveSystem<UiBindEntity>
        where TViewModel : struct, IViewModel
    {
        private readonly IContext<UiBindEntity> _context;
        private readonly BindingPath _modelPath;

        protected InitModelSystem(IContext<UiBindEntity> context)
            : this(context, typeof(TViewModel).Name){}

        protected InitModelSystem(IContext<UiBindEntity> context, BindingPath modelPath) : base(context)
        {
            _context = context;
            _modelPath = modelPath;
        }

        protected sealed override ICollector<UiBindEntity> GetTrigger(IContext<UiBindEntity> context) =>
            context.CreateCollector(UiBindMatcher.AllOf(UiBindMatcher.Model, UiBindMatcher.Initialized));

        protected sealed override bool Filter(UiBindEntity entity) =>
            entity.IsModel(_modelPath);

        protected sealed override void Execute(List<UiBindEntity> entities)
        {
            var model = _context.GetModel<TViewModel>(_modelPath);
            Execute(ref model);
            _context.Fetch(_modelPath, model);
        }

        protected abstract void Execute(ref TViewModel model);
    }
}