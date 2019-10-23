using System;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;

namespace UIDataBind.Entitas.Extensions
{
    public static class UIDataBindContextExtension
    {
        public static void InitModel<TViewModel>(this IContext<UiBindEntity> context, BindingPath path,
            TViewModel model) where TViewModel : struct, IViewModel =>
            context.Apply(engine => engine.Init(model, new ModelQuery(path, QueryType.Fetch)));

        public static TViewModel GetModel<TViewModel>(this IContext<UiBindEntity> context, BindingPath path,
            params BindingPath[] filter) where TViewModel : struct, IViewModel =>
            context.Apply(engine => engine.Get<TViewModel>(new ModelQuery(path, QueryType.Update, filter)));

        public static void Fetch<TViewModel>(this IContext<UiBindEntity> context, BindingPath path,
            TViewModel model, params BindingPath[] filter) where TViewModel : struct, IViewModel =>
            context.Apply(engine => engine.Apply(model, new ModelQuery(path, QueryType.Fetch, filter)));

        private static TViewModel Apply<TViewModel>(this IContext<UiBindEntity> context,
            Func<IECSEngine, TViewModel> method)
        {
            if (context is IEngineProvider engineProvider)
                return method.Invoke(engineProvider.GetEngine());
            throw new ArgumentException($"{context} must inherited {nameof(IEngineProvider)}", nameof(context));
        }
    }
}