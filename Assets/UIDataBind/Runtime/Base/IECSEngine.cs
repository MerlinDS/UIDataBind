namespace UIDataBind.Base
{
    public interface IEngineProvider
    {

    }
    public interface IECSEngine
    {
        IEntityProvider CreateBinderEntity(IBinder binder);
        void CreateModelEntity(BindingPath bindingPath);
        void CreateProperty(BindingPath propertyName);
        bool HasProperty<TValue>(BindingPath propertyName);
        void SetProperty<TValue>(BindingPath propertyName, TValue value);
        TValue GetPropertyValue<TValue>(BindingPath propertyName);

        int GetPropertyIndex<TValue>();
    }
}