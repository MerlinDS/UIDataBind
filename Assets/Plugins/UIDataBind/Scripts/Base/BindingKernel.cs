using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Plugins.UIDataBind.Components;
using Plugins.UIDataBind.Properties;
using UnityEngine;

namespace Plugins.UIDataBind.Base
{
    /// <summary>
    /// The kernel class of UIDataBinding.
    /// Responsible for collecting and storing <see cref="IViewContext"/>s with it binding properties.
    /// </summary>
    public class BindingKernel
    {
        private static BindingKernel _instance;
        private static readonly Type BindingPropertyType = typeof(IBindingProperty);

        private readonly Dictionary<int, ContextBindingPoint> _componentToContextMap;

        public static BindingKernel Instance => _instance ?? (_instance = new BindingKernel());

        private BindingKernel() =>
            _componentToContextMap = new Dictionary<int, ContextBindingPoint>();


        [CanBeNull]
        public IViewContext FindContext(BaseBinding binding) =>
            FindBindingPoint(binding).Context;

        [CanBeNull]
        public IBindingProperty FindProperty(BaseBinding binding, BindingPath path)
        {
            var bindingPoint = FindBindingPoint(binding);
            if (bindingPoint.IsEmpty)
                return null;

            var propertyName = path.PropertyName;
            return bindingPoint.Properties.Where(p => p.Name == propertyName)
                .Select(p => p.Instance)
                .FirstOrDefault();
        }

        private ContextBindingPoint FindBindingPoint(Component component)
        {
            var componentId = component.GetInstanceID();
            ContextBindingPoint point;
            if (_componentToContextMap.TryGetValue(componentId, out point))
                return point;

            point = CreateBindingPoint(component.GetComponentInParent<IViewContext>());
            _componentToContextMap.Add(componentId, point);

            return point;
        }


        private static ContextBindingPoint CreateBindingPoint(IViewContext context)
        {
            if (context == null)
                return default;

            var properties = GetBindingPropertiesFrom(context);
            return new ContextBindingPoint
            {
                Context = context,
                Properties = properties.ToArray()
            };
        }

        private static IEnumerable<PropertyPoint> GetBindingPropertiesFrom(IViewContext context)
        {
            return context.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => BindingPropertyType.IsAssignableFrom(f.FieldType))
                .Select(field => new PropertyPoint(field.Name, (IBindingProperty) field.GetValue(context)));
        }
    }
}