using System;
using UIDataBind.Converters;

namespace UIDataBind.Base
{
    public interface IEngineProvider
    {

    }
    public interface IECSEngine
    {
        IEntityProvider CreateBinderEntity(IBinder binder);
        void CreateModelEntity(BindingPath bindingPath);
        void CreateProperty(BindingPath propertyPath);
        bool HasProperty<TValue>(BindingPath propertyName);
        void SetProperty<TValue>(BindingPath propertyPath, TValue value);
        TValue GetPropertyValue<TValue>(BindingPath propertyPath);

        int GetPropertyIndex<TValue>();

        IConverters Converters { get; }
        Type[] PropertyTypes { get; }
        Type[] ComponentTypes { get; }
    }
}