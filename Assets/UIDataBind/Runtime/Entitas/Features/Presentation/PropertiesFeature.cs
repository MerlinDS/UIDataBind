using System;
using UIDataBind.Utils;
using UnityEngine;

namespace UIDataBind.Entitas.Features.Presentation
{
    public sealed class PropertiesFeature : Feature
    {
        public PropertiesFeature(UiBindContext context)
        {
            Add(new GetPropertyFromBinderSystem(context));
            Add(new GetPropertyDirectSystem<bool>(context));

            Add(new SetPropertyToBinderSystem(context));
            Add(new SetDirectValueBinderSystem<string>(context));
            Add(new SetDirectValueBinderSystem<bool>(context));
            Add(new SetDirectValueBinderSystem<int>(context));
            Add(new SetDirectValueBinderSystem<float>(context));
            Add(new SetDirectValueBinderSystem<Color>(context));
            Add(new SetDirectValueBinderSystem<Sprite>(context));
            Add(new SetDirectValueBinderSystem<Texture>(context));

            Add(new ConverterSystem<string, bool>(context, Convert.ToBoolean, Convert.ToString));
            Add(new ConverterSystem<string, int>(context, Convert.ToInt32, Convert.ToString));
            Add(new ConverterSystem<string, float>(context, Convert.ToSingle, Convert.ToString));
            Add(new ConverterSystem<bool, float>(context, Convert.ToSingle, Convert.ToBoolean));
            Add(new ConverterSystem<bool, int>(context, Convert.ToInt32, Convert.ToBoolean));
            Add(new ConverterSystem<int, float>(context, Convert.ToSingle, Convert.ToInt32));
            Add(new ConverterSystem<Sprite, string>(context, x => x.GetName(), Resources.Load<Sprite>));
            Add(new ConverterSystem<Texture, string>(context, x => x.GetName(), Resources.Load<Texture>));

            Add(new SetConvertedValueSystem(context));
        }
    }
}