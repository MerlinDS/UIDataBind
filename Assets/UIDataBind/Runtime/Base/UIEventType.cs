using System;

namespace UIDataBind.Base
{
    [Flags]
    public enum UIEventType
    {
        Click = 1,
        PointerEnter = 1 << 1,
        PointerExit = 1 << 2,
        Changed = 1 << 3,
    }
}