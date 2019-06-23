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

        private readonly BooleanProperty _sampleImageVisibleProperty = new BooleanProperty(true);

        public bool SampleImageVisible
        {
            get => _sampleImageVisibleProperty.Value;
            set => _sampleImageVisibleProperty.Value = value;
        }

        private readonly StringProperty _sampleStringProperty = new StringProperty("Some sample string from code");
        public string SampleString
        {
            get => _sampleStringProperty.Value;
            set => _sampleStringProperty.Value = value;
        }
    }
}