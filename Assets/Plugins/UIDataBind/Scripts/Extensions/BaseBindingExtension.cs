using System;
using JetBrains.Annotations;
using Plugins.UIDataBind.Base;
using Plugins.UIDataBind.Components;
using Plugins.UIDataBind.Properties;

namespace Plugins.UIDataBind.Extensions
{
    public static class BaseBindingExtension
    {
        [CanBeNull]
        public static IBindingProperty<TValue> FindBindingProperty<TValue>([NotNull] this BaseBinding binding,
            BindingPath path)
        {
            if (binding == null)
                throw new ArgumentNullException(nameof(binding));

            if(path.IsEmpty())
                return new BaseBindingProperty<TValue>();

            return BindingKernel.Instance.FindProperty(binding, path) as IBindingProperty<TValue>;
        }

        [CanBeNull]
        public static IViewContext FindContext([NotNull] this BaseBinding binding)
        {
            if (binding == null)
                throw new ArgumentNullException(nameof(binding));

            return BindingKernel.Instance.FindContext(binding);
        }

        [NotNull]
        public static string FindContextName([NotNull] this BaseBinding binding)
        {
            if (binding == null)
                throw new ArgumentNullException(nameof(binding));

            return BindingKernel.Instance.FindContext(binding)?.ToString() ?? "NULL";
        }
    }
}