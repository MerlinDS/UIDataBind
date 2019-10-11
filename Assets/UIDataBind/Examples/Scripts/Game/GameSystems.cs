using Entitas;
using JetBrains.Annotations;

namespace UIDataBind.Examples.Game
{
    [UsedImplicitly]
    public sealed class GameSystems : Systems
    {
        public GameSystems(Contexts contexts)
        {
            Add(new UIFeature.Systems.UIFeature(contexts));
        }
    }
}