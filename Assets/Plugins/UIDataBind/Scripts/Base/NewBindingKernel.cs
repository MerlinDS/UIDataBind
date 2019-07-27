using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Plugins.UIDataBind.Base
{
    public partial class BindingKernel : IDisposable
    {
        private readonly Dictionary<int, BindingScheme> _bindingSchemes = new Dictionary<int, BindingScheme>();

        public void Register(int instanceId, IViewContext context)
        {
            _bindingSchemes.Add(instanceId, context.CreateScheme());
        }

        [CanBeNull]
        public IViewContext GetContext(int instanceId)
        {
            if (!_bindingSchemes.ContainsKey(instanceId))
                return null;
            return (IViewContext) _bindingSchemes[instanceId].Instance;
        }

        public void Unregister(int instanceId)
        {
            _bindingSchemes.Remove(instanceId);
        }

        public void Dispose()
        {
            _bindingSchemes.Clear();
        }
    }

    public struct BindingScheme
    {
        public object Instance;
        public Dictionary<string, object> Members;
    }

    public static class BindingSchemeExtension
    {
        public static BindingScheme CreateScheme(this IViewContext context)
        {
            return new BindingScheme
            {
                Instance = context
            };
        }
    }
}