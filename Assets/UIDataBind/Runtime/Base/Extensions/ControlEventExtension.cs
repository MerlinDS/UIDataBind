namespace UIDataBind.Base.Extensions
{
    public static class ControlEventExtension
    {
        public static bool IsInvoked(this ControlEvent controlEvent)
            => controlEvent != ControlEvent.None;
    }
}