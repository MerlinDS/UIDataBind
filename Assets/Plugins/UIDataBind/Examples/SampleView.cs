using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Properties;
using UnityEngine;

namespace Plugins.UIDataBind.Examples
{
    public class SampleView : MonoBehaviour, IViewContext
    {

#pragma warning disable 0649
        [SerializeField]
        private Sprite _testSprite;
#pragma warning restore 0649

        private void Start()
        {
            SampleSprite = _testSprite;
            SampleSpritePath = _testSprite.name;
        }

        public void SwitchInt() => SampleInt = SampleInt > 0 ? 0 : 1;

        #region Properties

        [BindingProperty]
        private readonly BooleanProperty _visibleProperty = new BooleanProperty(true);

        public bool Visible
        {
            get => _visibleProperty.Value;
            set => _visibleProperty.Value = value;
        }

        [BindingProperty("ImageVisible")]
        private readonly BooleanProperty _sampleImageVisibleProperty = new BooleanProperty(true);

        public bool SampleImageVisible
        {
            get => _sampleImageVisibleProperty.Value;
            set => _sampleImageVisibleProperty.Value = value;
        }

        [BindingProperty]
        private readonly StringProperty _sampleStringProperty = new StringProperty("Some sample string from code");
        public string SampleString
        {
            get => _sampleStringProperty.Value;
            set => _sampleStringProperty.Value = value;
        }

        [BindingProperty]
        private readonly IntProperty _sampleIntProperty = new IntProperty(10);

        public int SampleInt
        {
            get => _sampleIntProperty.Value;
            set => _sampleIntProperty.Value = value;
        }

        [BindingProperty]
        private readonly SpriteProperty _sampleSpriteProperty = new SpriteProperty();

        public Sprite SampleSprite
        {
            get => _sampleSpriteProperty.Value;
            set => _sampleSpriteProperty.Value = value;
        }

        [BindingProperty]
        private readonly StringProperty _sampleSpritePathProperty = new StringProperty();

        public string SampleSpritePath
        {
            get => _sampleSpritePathProperty.Value;
            set => _sampleSpritePathProperty.Value = value;
        }

        #endregion
    }
}