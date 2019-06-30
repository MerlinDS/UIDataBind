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
            BindingPath path, TValue defaultValue = default)
        {
            if (binding == null)
                throw new ArgumentNullException(nameof(binding));

            if (path.Type == BindingType.None || path.IsEmpty())
                return new BaseBindingProperty<TValue>(defaultValue);

            var bindingProperty = BindingKernel.Instance.FindProperty(binding, path);
            if (bindingProperty == null || bindingProperty is IBindingProperty<TValue>)
                return bindingProperty as IBindingProperty<TValue>;


            return bindingProperty.IsConvertible<TValue>()
                ? new BindingPropertyAdapter<TValue>(bindingProperty)
                : null;
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