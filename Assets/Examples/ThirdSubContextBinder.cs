using Plugins.UIDataBind.Binders;
using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Properties;
using UnityEngine;

namespace Plugins.UIDataBind.Examples
{
    public class ThirdSubContextBinder : DataContextBinder<ThirdSubContext>{}

    public class ThirdSubContext: ISampleSubDataContext, IInitializable
    {
        public string Label => "Other stuffs (DOTS integration)";
        public void Init()
        {
            Debug.Log($"Init {nameof(ThirdSubContext)}");
        }

        [Bind]
        private readonly BooleanProperty _visible = new BooleanProperty();
        public bool Visible
        {
            get => _visible.Value;
            set => _visible.Value = value;
        }
    }
}