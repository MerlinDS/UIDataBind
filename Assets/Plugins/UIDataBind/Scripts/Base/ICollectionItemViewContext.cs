using System;

namespace Plugins.UIDataBind.Base
{
    public interface ICollectionItemViewContext : IViewContext, IDisposable
    {
        void Configure(object data);
    }
}