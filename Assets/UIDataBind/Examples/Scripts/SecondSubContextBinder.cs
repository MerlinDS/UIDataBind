using Plugins.UIDataBind.Binders;
using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Properties;
using UnityEngine;

namespace UIDataBind.Examples
{
    public class SecondSubContextBinder : DataContextBinder<SecondSubContext>{}

    public class SecondSubContext: ISampleSubDataContext, IInitializable
    {
        public string Label => "Collection binders";
        public void Init()
        {
            Debug.Log($"Init {nameof(SecondSubContext)}");
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