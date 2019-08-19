using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Properties;
using UnityEngine;

namespace Plugins.UIDataBind.Examples
{
    public class SampleMainContext : IDataContext, IInitializable
    {
        public void Init()
        {
            Sprite = Resources.Load<Sprite>("UIDataBind Icon");
            SpritePath = "UIBound Icon";
        }

        #region Bind Properties

        [Bind(help: "Visibility of main context")]
        private readonly BooleanProperty _visibleProperty = new BooleanProperty(true);

        public bool Visible
        {
            get => _visibleProperty.Value;
            set => _visibleProperty.Value = value;
        }

        [Bind(help: "Test counter")]
        private readonly BindProperty<int> _counterProperty = new BindProperty<int>(1);

        public int Counter
        {
            get => _counterProperty.Value;
            set => _counterProperty.Value = value;
        }

        [Bind(help: "Test image sprite")]
        private readonly BindProperty<Sprite> _spriteProperty = new BindProperty<Sprite>();

        public Sprite Sprite
        {
            get => _spriteProperty.Value;
            set => _spriteProperty.Value = value;
        }

        [Bind(help: "Test image path")]
        private readonly BindProperty<string> _spritePathProperty = new BindProperty<string>();

        public string SpritePath
        {
            get => _spritePathProperty.Value;
            set => _spritePathProperty.Value = value;
        }

        #endregion
    }
}