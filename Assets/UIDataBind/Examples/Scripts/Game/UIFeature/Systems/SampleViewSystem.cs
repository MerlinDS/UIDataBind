using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public abstract class SampleViewSystem<TModel> : ReactiveSystem<UiBindEntity>
        where TModel : struct, IViewModel
    {
        private TModel _viewModel;
        private readonly IProperties _properties;

        protected SampleViewSystem(UiBindContext context, BindingPath modelPath) : base(context)
        {
            ModelPath = modelPath;
            _viewModel = new TModel();
            _properties = context.GetEngine().GetProperties(modelPath);
        }

        protected BindingPath ModelPath { get; }

        protected sealed override void Execute(List<UiBindEntity> entities)
        {
            _properties.UpdateModel(ref _viewModel);
            foreach (var entity in entities)
                Execute(ref _viewModel, entity);
            _properties.Fetch(_viewModel);
        }

        protected abstract void Execute(ref TModel viewModel, UiBindEntity entity);
    }
}