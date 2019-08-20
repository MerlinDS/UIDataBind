using JetBrains.Annotations;
using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Properties;
using UnityEngine;

namespace Plugins.UIDataBind.Examples
{
    public class SampleMainContext : IDataContext, IInitializable
    {
        private const string UIDataBindIcon = "UIDataBind Icon";
        private const string UIBoundIcon = "UIBound Icon";

        private Color _firstColor;
        private Color _secondColor;
        public void Configure(Color firstColor, Color secondColor)
        {
            _firstColor = firstColor;
            _secondColor = secondColor;
        }
        public void Init()
        {
            Debug.Log("Init");
            TestMethod();
        }

        [Bind(help: "Test method to invoke from binder"), UsedImplicitly]
        private void TestMethod()
        {
            SpritePath = SpritePath == UIBoundIcon ? UIDataBindIcon : UIBoundIcon;
            NextSpritePath = SpritePath == UIBoundIcon ? UIDataBindIcon : UIBoundIcon;
            Resources.UnloadAsset(Sprite);
            Sprite = Resources.Load<Sprite>(SpritePath);
            Color = Color == _firstColor ? _secondColor : _firstColor;
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

        [Bind]
        private readonly BindProperty<string> _nextSpritePathProperty = new BindProperty<string>();

        public string NextSpritePath
        {
            get => _nextSpritePathProperty.Value;
            set => _nextSpritePathProperty.Value = value;
        }

        [Bind]
        private readonly BindProperty<Color> _colorProperty = new BindProperty<Color>();

        public Color Color
        {
            get => _colorProperty.Value;
            set => _colorProperty.Value = value;
        }

        #endregion

    }
}