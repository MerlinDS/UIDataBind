using System;

namespace UIDataBind.Base
{
    [Flags]
    public enum ControlEvent
    {
        None = 0x0,
        Click = 1,
        PointerEnter = 1 << 1,
        PointerExit = 1 << 2,
        Changed = 1 << 3,
    }
}