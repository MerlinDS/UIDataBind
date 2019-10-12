using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Entitas;
using JetBrains.Annotations;
using UIDataBind.Base;
using UIDataBind.Base.Components;
using UIDataBind.Entitas;

namespace UIDataBind.Entitas
{
    public sealed class EntitasEntityManager : IEntityManager
    {
        private static readonly Type InterfaceType = typeof(IPropertyComponent);
        private static readonly string InterfaceName = typeof(IPropertyComponent<>).Name;

        /// <summary>
        /// <see cref="IPropertyComponent{TValue}"/> component indices in UiBind <see cref="IContext"/>
        /// </summary>
        private readonly int[] _indices;

        /// <summary>
        /// <see cref="IPropertyComponent{TValue}"/> component TValue type in UiBind <see cref="IContext"/>
        /// </summary>
        private readonly Type[] _types;


        public EntitasEntityManager([NotNull] IContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (context.contextInfo.name != "UiBind")
                throw new ArgumentException($"Wrong context {context}! A context must have UiBind name",
                                            nameof(context));

            //Collect type data from IPropertyComponents in current context
            var indices = new List<int>();
            var types = new List<Type>();
            var componentTypes = context.contextInfo.componentTypes;
            for (var index = 0; index < componentTypes.Length; index++)
            {
                var componentType = componentTypes[index];
                if (!InterfaceType.IsAssignableFrom(componentType))
                    continue;

                types.Add(GetPropertyValueType(componentType));
                indices.Add(index);
            }

            _indices = indices.ToArray();
            _types = types.ToArray();
            Context = context;
        }

        /// <summary>
        /// UiDataBind Entitas <see cref="IContext"/>
        /// </summary>
        public IContext Context { get; }

        #region API

        public IUiBindEntity CreateEntity() =>
            (Context as UiBindContext)?.CreateEntity();

        public void DestroyEntity(IUiBindEntity entity) =>
            ((IEntity) entity).Destroy();

        /// <summary>
        /// Check of entity has <see cref="IPropertyComponent{TValue}"/> with a specified type
        /// </summary>
        /// <param name="entity">an <see cref="IEntity"/> to check </param>
        /// <typeparam name="T">TValue type of a <see cref="IPropertyComponent{TValue}"/></typeparam>
        /// <returns>True if an <see cref="IEntity"/> has component with a specified type </returns>
        public bool HasComponent<T>([NotNull] IUiBindEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return ((IEntity) entity).HasComponent(GetPropertyComponentIndex<T>());
        }

        public void AddComponent<T>([NotNull] IUiBindEntity entity, T value)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var componentIndex = GetPropertyComponentIndex<T>();
            var componentType = Context.contextInfo.componentTypes[componentIndex];
            var component = (IPropertyComponent<T>) ((IEntity) entity).CreateComponent(componentIndex, componentType);
            component.Value = value;
            ((IEntity) entity).AddComponent(componentIndex, component as IComponent);
        }

        public void SetComponentData<T>([NotNull] IUiBindEntity entity, T value)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var componentIndex = GetPropertyComponentIndex<T>();
            var component = (IPropertyComponent<T>) ((IEntity) entity).GetComponent(componentIndex);
            component.Value = value;
            ((IEntity) entity).ReplaceComponent(componentIndex, component as IComponent);
        }

        public void RemoveComponent<T>([NotNull] IUiBindEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            ((IEntity) entity).RemoveComponent(GetPropertyComponentIndex<T>());
        }

        public T GetComponentData<T>([NotNull] IUiBindEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var componentIndex = GetPropertyComponentIndex<T>();
            if (!((IEntity) entity).HasComponent(componentIndex))
                return default;

            var component = (IPropertyComponent<T>) ((IEntity) entity).GetComponent(componentIndex);
            return component.Value;
        }

        public Type GetComponentDataType(IUiBindEntity entity)
        {
            var e = (IEntity) entity;
            for (var index = 0; index < _indices.Length; index++)
            {
                if (e.HasComponent(_indices[index]))
                    return _types[index];
            }

            throw new AggregateException($"{entity} does not has any of PropertyComponent<TValue> component");
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Get an index of a <see cref="IPropertyComponent{TValue}"/> in <see cref="IContext"/>
        /// </summary>
        /// <typeparam name="TValue">TValue <see cref="Type"/> of a <see cref="IPropertyComponent{TValue}"/></typeparam>
        /// <returns>component index in UiBind <see cref="IContext"/></returns>
        /// <exception cref="ArgumentException">A component was not generated</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetPropertyComponentIndex<TValue>()
        {
            var index = Array.IndexOf(_types, typeof(TValue));
            if (index < 0)
                throw new ArgumentException(
                    $"Cannot add PropertyComponent<{typeof(TValue)}>. Such a component was not generated!");
            return _indices[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Type GetPropertyValueType(Type componentType) =>
            componentType.GetInterface(InterfaceName).GetGenericArguments().First();

        #endregion
    }
}

public sealed partial class UiBindEntity : IUiBindEntity
{
}

public sealed partial class UiBindContext
{
    private IEntityManager _entityManager;
    public IEntityManager EntityManager => _entityManager ?? (_entityManager = new EntitasEntityManager(this));
}