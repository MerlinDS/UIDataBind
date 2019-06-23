using Plugins.UIDataBind.Properties;
using UnityEngine;

namespace Plugins.UIDataBind.Examples
{
    public class SampleView : MonoBehaviour, IViewContext
    {
        private readonly BooleanProperty _visibleProperty = new BooleanProperty(true);

        public bool Visible
        {
            get => _visibleProperty.Value;
            set => _visibleProperty.Value = value;
        }

        private readonly BooleanProperty _smapleImageVisibleProperty = new BooleanProperty(true);

        public bool SampleImageVisible
        {
            get => _smapleImageVisibleProperty.Value;
            set => _smapleImageVisibleProperty.Value = value;
        }
    }
}