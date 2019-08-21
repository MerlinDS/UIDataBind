using System;
using UIDataBindCore;

namespace Plugins.UIDataBind.Binders
{
    public interface IDataContextBinder : IBinder
    {
        Type ContextType { get; }
    }
}