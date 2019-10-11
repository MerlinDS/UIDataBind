using Entitas;
using UIDataBind.Base;

namespace UIDataBind.Entitas.Wrappers
{
    public class EntitasProvider : IEntityProvider
    {
        public IEntity Entity;
        public int PropertyComponentIndex;
    }
}