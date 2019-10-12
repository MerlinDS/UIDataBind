using System;

namespace UIDataBind.Base
{
    public interface IUiBindEntity
    {
    }

    public interface IEntityManager
    {
        IUiBindEntity CreateEntity();
        void DestroyEntity(IUiBindEntity entity);
        bool HasComponent<T>(IUiBindEntity entity);
        void AddComponent<T>(IUiBindEntity entity, T value);
        void RemoveComponent<T>(IUiBindEntity entity);

        void SetComponentData<T>(IUiBindEntity entity, T value);
        T    GetComponentData<T>(IUiBindEntity entity);

        Type GetComponentDataType(IUiBindEntity entity);
    }
}