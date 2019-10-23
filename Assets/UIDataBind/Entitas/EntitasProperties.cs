using System;
using System.Runtime.CompilerServices;
using UIDataBind.Base;

namespace UIDataBind.Entitas
{
    internal sealed class EntitasProperties : IProperties
    {
        private readonly IECSEngine _engine;
        private ModelQuery _query;

        public EntitasProperties(IECSEngine engine) =>
            _engine = engine;

        public void SetQuery(ModelQuery query) =>
            _query = query;

        public void Refresh<TValue>(BindingPath propertyName, ref TValue value)
        {
            if (!IsFiltered(propertyName))
                return;

            propertyName = BuildPath(propertyName);
            switch (_query.Type)
            {
                case QueryType.Update:
                {
                    if (_engine.HasProperty<TValue>(propertyName))
                        value = _engine.GetPropertyValue<TValue>(propertyName);
                    return;
                }
                case QueryType.Fetch:
                    if (!_engine.HasProperty<TValue>(propertyName))
                    {
                        _engine.CreateProperty(propertyName);
                        _engine.SetProperty(propertyName, value);
                        return;
                    }

                    var propertyValue = _engine.GetPropertyValue<TValue>(propertyName);
                    if (Equals(propertyValue, value))
                        return;

                    _engine.SetProperty(propertyName, value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_query.Type), _query.Type, null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BindingPath BuildPath(BindingPath propertyName)
            => BindingPath.BuildFrom(_query.Path, propertyName);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsFiltered(BindingPath propertyName) =>
            _query.Type != QueryType.None &&
            (!(_query.Filter?.Length > 0) || Array.IndexOf(_query.Filter, propertyName) >= 0);
    }
}