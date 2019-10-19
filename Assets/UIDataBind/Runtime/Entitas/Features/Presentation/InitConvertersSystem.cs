using System;
using Entitas;
using UIDataBind.Base.Extensions;
using UIDataBind.Converters;
using UnityEngine;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class InitConvertersSystem : IInitializeSystem
    {
        private readonly IConverters _converters;

        public InitConvertersSystem(UiBindContext context)
        {
            _converters = context.GetEngine().Converters;
        }

        public void Initialize()
        {
            _converters.Register((bool x)=>Convert.ToString(x));
            _converters.Register((bool x)=>Convert.ToInt32(x));
            _converters.Register((bool x)=>Convert.ToSingle(x));

            _converters.Register((int x)=>Convert.ToSingle(x));
            _converters.Register((int x)=>Convert.ToBoolean(x));
            _converters.Register((int x)=>Convert.ToString(x));

            _converters.Register((string x)=>Convert.ToSingle(x));
            _converters.Register((string x)=>Convert.ToInt32(x));
            _converters.Register((string x)=>Convert.ToBoolean(x));
            _converters.Register((string x)=>Resources.Load<Sprite>(x));
            _converters.Register((string x)=>Resources.Load<Texture>(x));
        }
    }
}