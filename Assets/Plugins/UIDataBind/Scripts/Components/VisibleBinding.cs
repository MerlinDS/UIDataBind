using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Components
{
    [AddComponentMenu("UIDataBind/Visible", 0)]
    public sealed class VisibleBinding : BasePropertyBinding<bool>
    {
        #region ComponentScope

        private readonly HashSet<Graphic> _graphics = new HashSet<Graphic>();
        private readonly HashSet<LayoutElement> _layouts = new HashSet<LayoutElement>();

        #endregion

        private readonly HashSet<VisibleBinding> _bindings = new HashSet<VisibleBinding>();
        private readonly Queue<Transform> _transformsBuffer = new Queue<Transform>();

        private bool _isDirty = true;


        protected override void UpdateValueHandler(bool value)
        {
            if (_isDirty)
                UpdateCache();

            foreach (var graphic in _graphics)
                graphic.enabled = value;

            foreach (var layout in _layouts)
                layout.ignoreLayout = !value;

            foreach (var binding in _bindings)
                binding.UpdateValueHandler(value && binding.Value);

        }

        private void UpdateCache()
        {
            _isDirty = false;
            _transformsBuffer.Enqueue(transform);
            while (_transformsBuffer.Count > 0)
            {
                var component = _transformsBuffer.Dequeue();
                AddChildren(component, _transformsBuffer);
                if (component != transform)
                {
                    if(CollectComponents(component, _bindings, true))
                        continue;
                }

                CollectComponents(component, _graphics);
                CollectComponents(component, _layouts);
            }
        }

        private static void AddChildren(IEnumerable parent, Queue<Transform> buffer)
        {
            foreach (Component child in parent)
                buffer.Enqueue((Transform) child);
        }

        private static bool CollectComponents<TComponent>(Component container, ICollection<TComponent> cacheBuffer,
            bool onlyEnabled = false) where TComponent : Behaviour
        {
            if(container == null)
                return false;

            var components = container.GetComponents<TComponent>();
            var length = cacheBuffer.Count;
            foreach (var component in components)
            {
                if(!onlyEnabled || component.enabled)
                    cacheBuffer.Add(component);
            }

            return cacheBuffer.Count != length;
        }

        [UsedImplicitly]
        private void CleanCache()
        {
            _graphics.Clear();
            _bindings.Clear();
            _layouts.Clear();
            _isDirty = true;
        }

#if UNITY_EDITOR
        [ContextMenu("ChangeVisibility")]
        private void ChangeVisibility() => Value = !Value;
#endif
    }
}