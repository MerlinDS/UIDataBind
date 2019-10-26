using System;

namespace UIDataBind.Binders.Attributes
{
    [AttributeUsage(AttributeTargets.Struct)]
    public class ViewModelAttribute : Attribute
    {
        public string[] Names { get; }
        public ViewModelAttribute(params string[] names) =>
            Names = names;
    }
}