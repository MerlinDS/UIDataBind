using Plugins.UIDataBind.Binders;
using UnityEngine;

namespace Plugins.UIDataBind.Examples
{
    public sealed class SampleMainContextBinder : DataContextBinder<SampleMainContext>
    {
        [SerializeField]
        private Color _firstColor;
        [SerializeField]
        private Color _secondColor;

        public override void Bind()
        {
            Context.Configure(_firstColor, _secondColor);
            base.Bind();
        }
    }
}
