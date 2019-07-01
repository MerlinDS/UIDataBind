using JetBrains.Annotations;
using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Properties;
using UnityEngine;

namespace Plugins.UIDataBind.Examples
{
    public class SampleSubView : MonoBehaviour, IViewContext
    {
        [UsedImplicitly, BindingMethod("Image Visibility Change")]
        private void OnImageVisibilityChange() =>
            ImageVisible = !ImageVisible;

        [UsedImplicitly,BindingMethod("Image Color Change")]
        private void OnImageColorChange() =>
            ImageColor = ImageColor == Color.white ? Color.red : Color.white;

        [UsedImplicitly,BindingMethod("Image Label Change")]
        private void OnImageLabelChange() =>
            ImageValue++;

        #region Properties

        [BindingProperty]
        private readonly BooleanProperty _visibleProperty = new BooleanProperty(true);

        public bool Visible
        {
            get => _visibleProperty.Value;
            set => _visibleProperty.Value = value;
        }

        [BindingProperty("Plugin Image Visibility")]
        private readonly BooleanProperty _imageVisibleProperty = new BooleanProperty(true);

        public bool ImageVisible
        {
            get => _imageVisibleProperty.Value;
            set => _imageVisibleProperty.Value = value;
        }


        [BindingProperty("Plugin Image Color")]
        private readonly ColorProperty _imageColorProperty = new ColorProperty(Color.white);

        public Color ImageColor
        {
            get => _imageColorProperty.Value;
            set => _imageColorProperty.Value = value;
        }

        [BindingProperty("Plugin Image Value")]
        private readonly IntProperty _imageLabelProperty = new IntProperty();

        public int ImageValue
        {
            get => _imageLabelProperty.Value;
            set => _imageLabelProperty.Value = value;
        }

        #endregion
    }
}