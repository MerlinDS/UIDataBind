using JetBrains.Annotations;
using Plugins.UIDataBind.Binders;
using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Properties;
using UnityEngine;

namespace Plugins.UIDataBind.Examples
{
    public class FirstSubContextBinder : DataContextBinder<FirstSubContext>{}

    public class FirstSubContext: IVisibleDataContext, IInitializable
    {

        public void Init()
        {
            Debug.Log($"Init {nameof(FirstSubContext)}");
            Icon = Resources.Load<Sprite>("UIDataBind Icon");
        }

        [Bind, UsedImplicitly]
        private void ChangeToGreen() =>
            IconColor = Color.green;

        [Bind, UsedImplicitly]
        private void ChangeToRed() =>
            IconColor = Color.red;

        [Bind, UsedImplicitly]
        private void ChangeToWight() =>
            IconColor = Color.white;

        [Bind, UsedImplicitly]
        private void SwitchIconVisible() =>
            IconVisible = !IconVisible;

        [Bind]
        private readonly BooleanProperty _visibleProperty = new BooleanProperty(true);
        public bool Visible
        {
            get => _visibleProperty.Value;
            set => _visibleProperty.Value = value;
        }

        [Bind, UsedImplicitly]
        private readonly FloatProperty _sliderProperty = new FloatProperty(5);

        [Bind]
        private readonly BooleanProperty _iconVisibleProperty = new BooleanProperty(true);
        [Bind]
        private readonly StringProperty _iconVisibleLabelProperty = new StringProperty("Hide");

        private bool IconVisible
        {
            get => _iconVisibleProperty.Value;
            set
            {
                _iconVisibleProperty.Value = value;
                _iconVisibleLabelProperty.Value = value ? "Hide" : "Show";
            }
        }

        [Bind]
        private readonly BindProperty<Sprite> _iconProperty = new BindProperty<Sprite>();
        private Sprite Icon
        {
            get => _iconProperty.Value;
            set => _iconProperty.Value = value;
        }

        [Bind]
        private readonly BindProperty<Color> _iconColorProperty = new BindProperty<Color>(Color.white);

        private Color IconColor
        {
            get => _iconColorProperty.Value;
            set
            {
                _iconColorProperty.Value = value;
                IconVisible = true;
            }
        }

        [Bind]
        private readonly BooleanProperty _textChangeableProperty = new BooleanProperty();
        [Bind]
        private readonly StringProperty _textProperty = new StringProperty();
    }
}