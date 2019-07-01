using System;

namespace Plugins.UIDataBind.Base
{
    /// <summary>
    /// Use to select how to bind a component during play mode.
    /// </summary>
    ///
    /// <seealso cref="BindingType.None"/>
    /// <seealso cref="BindingType.Context"/>
//    /// <seealso cref="BindingType.Condition"/>
    public enum BindingType
    {
        /// <summary>
        /// No binding. A binder will use its own value on binding action.
        /// <seealso cref="Plugins.UIDataBind.Components.BaseBinding"/>
        /// <seealso cref="Plugins.UIDataBind.Components.BasePropertyBinding{T}.Value">BasePropertyBinding.Value</seealso>
        /// </summary>
        None,
        /// <summary>
        /// The binding to a <see cref="IViewContext"/>
        /// <see cref="Plugins.UIDataBind.Properties.BaseBindingProperty">bindingProperties</see>
        /// and <see cref="System.Action">methods</see>
        /// </summary>
        Context,
        /*//TODO: A condition need to be implemented
        /// <summary>
        /// The binding to condition components
        /// </summary>
        Condition*/
    }

    public static class BindingTypeExtension
    {
        public static string GetHelpString(this BindingType type)
        {
            switch (type)
            {
                case BindingType.None:
                    return "No binding. A binder will use its own value on binding action.";
                case BindingType.Context:
                    return "The binding to a IViewContext bindingProperties and methods";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}