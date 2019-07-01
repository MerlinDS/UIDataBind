using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Plugins.UIDataBind.Attributes;
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
        private const BindingFlags MethodFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance |
                                                 BindingFlags.DeclaredOnly;


        private static readonly Type BindingMethodAttribute = typeof(BindingActionAttribute);
        private static readonly Type BindingPropertyType = typeof(IBindingProperty);

        private static BindingKernel _instance;

        private readonly Dictionary<int, ContextBindingPoint> _contextBindingPoints;
        private readonly Dictionary<int, int> _componentToContextMap;

        public static BindingKernel Instance => _instance ?? (_instance = new BindingKernel());

        private BindingKernel()
        {
            _contextBindingPoints = new Dictionary<int, ContextBindingPoint>();
            _componentToContextMap = new Dictionary<int, int>();
        }


        [CanBeNull]
        public IViewContext FindContext(BaseBinding binding) =>
            FindBindingPoint(binding).Context;

        [CanBeNull]
        public TValue Find<TValue>(BaseBinding binding, BindingPath path)
        {
            var bindingPoint = FindBindingPoint(binding);
            if (bindingPoint.IsEmpty)
                return default;

            var points = typeof(TValue) == typeof(Action)
                ? bindingPoint.Methods
                : bindingPoint.Properties;

            var name = path.Name;
            var point = points.FirstOrDefault(p => p.Name == name);
            return (TValue) point.Instance;
        }


        private ContextBindingPoint FindBindingPoint(Component component)
        {
            int contextId;
            var componentId = component.GetInstanceID();
            if (_componentToContextMap.TryGetValue(componentId, out contextId))
                return _contextBindingPoints[contextId];

            ContextBindingPoint point;
            var context = component.GetComponentInParent<IViewContext>();
            contextId = context.GetHashCode();

            if (!_contextBindingPoints.TryGetValue(contextId, out point))
            {
                point = CreateBindingPoint(context);
                _contextBindingPoints.Add(contextId, point);
            }
            _componentToContextMap.Add(componentId, contextId);
            return point;
        }


        private static ContextBindingPoint CreateBindingPoint(IViewContext context)
        {
            if (context == null)
                return default;

            var methods = GetBindingMethodsFrom(context).ToArray();
            var properties = GetBindingPropertiesFrom(context).ToArray();
            return new ContextBindingPoint
            {
                Context = context,
                Methods = methods,
                Properties = properties
            };
        }

        private static IEnumerable<InstancePoint> GetBindingPropertiesFrom(IViewContext context)
        {
            return context.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => BindingPropertyType.IsAssignableFrom(f.FieldType))
                .Select(field => new InstancePoint(field.Name, (IBindingProperty) field.GetValue(context)));
        }

        private static IEnumerable<InstancePoint> GetBindingMethodsFrom(IViewContext context)
        {
            return context.GetType().GetMethods(MethodFlags)
                .Where(method => Attribute.IsDefined(method, BindingMethodAttribute))
                .Where(method => method.GetParameters().Length == 0)
                .Where(method => method.ReturnType == typeof(void))
                .Select(method => new InstancePoint(method.Name, CreateDelegate(method, context)));
        }

        private static Action CreateDelegate(MethodInfo methodInfo, object target) =>
            (Action) methodInfo.CreateDelegate(typeof(Action), target);
    }
}