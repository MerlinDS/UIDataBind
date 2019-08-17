using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Properties;

namespace Plugins.UIDataBind.Examples
{
    public class SampleMainContext : IDataContext
    {
        #region Bind Properties

        [Bind("Visible")]
        private readonly BindProperty<bool> _visibleProperty = new BindProperty<bool>(true);

        public bool Visible
        {
            get => _visibleProperty.Value;
            set => _visibleProperty.Value = value;
        }

        #endregion
    }
}