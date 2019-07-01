using JetBrains.Annotations;
using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Properties;
using UnityEngine;

namespace Plugins.UIDataBind.Examples
{
    public class SampleView : MonoBehaviour, IViewContext
    {
        [UsedImplicitly, BindingMethod("Visibility Change", "Will change a visibility of sub view")]
        private void OnVisibilityChanged() =>
            Visible = !Visible;

        #region Properties

        [BindingProperty(help:"Visibility flag of sub view")]
        private readonly BooleanProperty _visibleProperty = new BooleanProperty(true);

        public bool Visible
        {
            get => _visibleProperty.Value;
            set => _visibleProperty.Value = value;
        }

        #endregion
    }
}