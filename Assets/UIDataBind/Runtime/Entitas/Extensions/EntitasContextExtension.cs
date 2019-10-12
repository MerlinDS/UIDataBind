using System.Collections.Generic;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Entitas.Components;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntitasContextExtension
    {
        public static IEnumerable<IUiBindEntity> GetBoundedEntities(this IEntityManager manager, IUiBindEntity propertyEntity)
        {
            var pathComponent = (PathComponent)((IEntity) propertyEntity).GetComponent(UiBindComponentsLookup.Path);
            return ((EntityIndex<UiBindEntity, string>) ((EntitasEntityManager) manager).Context.GetEntityIndex(
                "BindingPath")).GetEntities(pathComponent.Value);
        }
    }
}