using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public abstract class SampleViewSystem<TModel> : ReactiveSystem<UiBindEntity>
        where TModel : struct, IViewModel
    {
        private readonly IProperties _properties;

        protected SampleViewSystem(UiBindContext context, BindingPath modelPath): base(context) =>
            _properties = context.GetProperties(modelPath);

        protected BindingPath ModelPath => _properties.ModelPath;
        protected TModel ViewModel => _properties.GetModel<TModel>();

        protected sealed override void Execute(List<UiBindEntity> entities)
        {
            var model = ViewModel;
            // ReSharper disable once UnusedVariable
            foreach (var entity in entities)
                Execute(ref model);
            _properties.Fetch(model);
        }

        protected abstract void Execute(ref TModel viewModel);
    }
}