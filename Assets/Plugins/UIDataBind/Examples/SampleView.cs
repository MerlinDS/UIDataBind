using Plugins.UIDataBind.Base;
using Plugins.UIDataBind.Properties;

namespace Plugins.UIDataBind.Examples
{
    public class SampleView : BaseView
    {
        private readonly BooleanProperty _visibleProperty = new BooleanProperty(true);
        public bool Visible
        {
            get => _visibleProperty.Value;
            set => _visibleProperty.Value = value;
        }
    }
}