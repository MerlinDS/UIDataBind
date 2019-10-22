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
        void CreateModelEntity(OldBindingPath bindingPath);
        void CreateProperty(OldBindingPath propertyPath);
        bool HasProperty<TValue>(OldBindingPath propertyName);
        void SetProperty<TValue>(OldBindingPath propertyPath, TValue value);
        TValue GetPropertyValue<TValue>(OldBindingPath propertyPath);

        int GetPropertyIndex<TValue>();

        IConverters Converters { get; }
        Type[] PropertyTypes { get; }
        Type[] ComponentTypes { get; }
    }
}