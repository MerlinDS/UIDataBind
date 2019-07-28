namespace UIDataBindCore.Extensions
{

    public static class DataContextExtension
    {
        /// <summary>
        /// Register <see cref="IDataContext"/> instance in the <see cref="BindingKernel"/>
        /// </summary>
        /// <param name="context">Instance of a <see cref="IDataContext"/></param>
        public static void Register(this IDataContext context) =>
            BindingKernel.Instance.Register(context);

        /// <summary>
        /// Remove <see cref="IDataContext"/> instance from the <see cref="BindingKernel"/>
        /// </summary>
        /// <param name="context">Instance of a <see cref="IDataContext"/></param>
        public static void Unregister(this IDataContext context) =>
            BindingKernel.Instance.Unregister(context);
    }
}