using Plugins.UIDataBind.Base;
using Plugins.UIDataBind.Utils;
using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    /// <summary>
    /// Helper <see cref="MonoBehaviour"/> for <see cref="ICollectionItemViewContext"/>
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public abstract class CollectionItemViewContext<TData> : MonoBehaviour, ICollectionItemViewContext
    {
        public void Configure(object data) =>
            Configure(ConvertUtils.SafeCast<TData>(data));

        protected abstract void Configure(TData data);

        private void OnDestroy() => Dispose();

        public virtual void Dispose()
        {

        }
    }
}