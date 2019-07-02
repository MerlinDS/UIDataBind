using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Plugins.UIDataBind.Components;
using Plugins.UIDataBind.Extensions;
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
            var point = points.FirstOrDefault(p => p.Item1 == name);
            return (TValue) point?.Item2;
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

            var methods = context.GetBindingMethods().ToArray();
            var properties = context.GetBindingProperties().ToArray();
            return new ContextBindingPoint
            {
                Context = context,
                Methods = methods,
                Properties = properties
            };
        }

        public void Register(int instanceId, IViewContext context)
        {
            throw new NotImplementedException();
        }

        public void Unregister(int instanceId, IViewContext context)
        {
            throw new NotImplementedException();
        }
    }
}