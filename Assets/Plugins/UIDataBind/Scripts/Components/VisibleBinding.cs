using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Components
{
    [AddComponentMenu("UIDataBind/Visible", 0)]
    public sealed class VisibleBinding : BasePropertyBindingBehaviour<bool>
    {
        private readonly List<Graphic> _graphicsCache = new List<Graphic>();
        private readonly List<LayoutElement> _layoutCache = new List<LayoutElement>();
        private readonly List<VisibleBinding> _visibleCache = new List<VisibleBinding>();

        private readonly Queue<Transform> _transforms = new Queue<Transform>();

        protected override void UpdateValueHandler(bool value)
        {
            UpdateCache();

            foreach (var graphic in _graphicsCache)
                graphic.enabled = value;

            foreach (var layout in _layoutCache)
                layout.ignoreLayout = !value;

            foreach (var binding in _visibleCache)
                binding.Value = value;
        }

        private void UpdateCache()
        {
            _graphicsCache.Clear();
            _visibleCache.Clear();
            _layoutCache.Clear();

            _transforms.Enqueue(transform);
            while (_transforms.Count > 0)
            {
                var component = _transforms.Dequeue();
                foreach (Component child in component)
                    _transforms.Enqueue((Transform) child);

                var visibleBinder = component.GetComponent<VisibleBinding>();
                if (!visibleBinder || !visibleBinder.enabled || component == transform)
                {
                    _graphicsCache.AddRange(GetAllGraphicComponents(component));
                    _layoutCache.AddRange(GetAllLayoutElementComponents(component));
                    continue;
                }

                _visibleCache.Add(visibleBinder);

            }
        }
        private static IEnumerable<Graphic> GetAllGraphicComponents(Component component) =>
            component.GetComponents<Graphic>();

        private static IEnumerable<LayoutElement> GetAllLayoutElementComponents(Component component) =>
            component.GetComponents<LayoutElement>();

#if UNITY_EDITOR
        [ContextMenu("ChangeVisibility")]
        private void ChangeVisibility() => Value = !Value;
#endif
    }
}