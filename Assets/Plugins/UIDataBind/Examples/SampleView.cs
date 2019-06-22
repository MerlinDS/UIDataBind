using Plugins.UIDataBind.Base;
using UnityEngine;

namespace Plugins.UIDataBind.Examples
{
    public class SampleView : BaseView
    {
        private bool _visible;
        private Sprite _sampleSprite;

        public bool Visible
        {
            get => _visible;
            set => _visible = value;
        }

        public Sprite SampleSprite
        {
            get => _sampleSprite;
            set => _sampleSprite = value;
        }
    }
}