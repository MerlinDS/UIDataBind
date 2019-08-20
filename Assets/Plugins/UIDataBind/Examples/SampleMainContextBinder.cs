using Plugins.UIDataBind.Binders;
using UnityEngine;

namespace Plugins.UIDataBind.Examples
{
    public sealed class SampleMainContextBinder : DataContextBinder<SampleMainContext>
    {
#pragma warning disable 0649
        [SerializeField]
        private Color _firstColor;
        [SerializeField]
        private Color _secondColor;
#pragma warning restore 0649


        public override void Bind()
        {
            Context.Configure(_firstColor, _secondColor);
            base.Bind();
        }
    }
}
