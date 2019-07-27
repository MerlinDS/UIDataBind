using System;

namespace UIDataBindCore.Attributes
{
    /// <summary>
    /// Use this attribute to bind members (properties, methods or collections) to UI components
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class BindAttribute : Attribute
    {
        /// <summary>
        /// The in-code name of a bound member
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The human readable pseudonym of a bound member
        /// </summary>
        public string Alias { get; private set; }
        /// <summary>
        /// The help string a bound member
        /// </summary>
        public string Help { get; private set; }

        public BindAttribute(string alias = default, string help = default)
        {
            Alias = alias;
            Help = help;
        }
    }
}