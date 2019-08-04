using System;
using UIDataBindCore.Properties;

namespace UIDataBindCore.Extensions
{
    public static class DataContextExtension
    {
        /// <summary>
        /// Register <see cref="IDataContext"/> instance in the <see cref="BindingKernel"/>
        /// </summary>
        /// <param name="context">Instance of a <see cref="IDataContext"/></param>
        public static void Register(this IDataContext context) =>
            Kernel.Register(context);

        /// <summary>
        /// Remove <see cref="IDataContext"/> instance from the <see cref="BindingKernel"/>
        /// </summary>
        /// <param name="context">Instance of a <see cref="IDataContext"/></param>
        public static void Unregister(this IDataContext context) =>
            Kernel.Unregister(context);

        public static IBindProperty<TValue> FinProperty<TValue>(this IDataContext context, string memberName)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(memberName))
                throw new ArgumentException(nameof(memberName));

            try
            {
                var sourceProperty = Kernel.FindProperty(context, memberName);
                return Kernel.ConversionMethods.AsPropertyOf<TValue>(sourceProperty) ?? new BindProperty<TValue>();
            }
            catch (InvalidOperationException exception)
            {
                if(exception.Source == nameof(BindingKernel))
                    throw new InvalidOperationException(exception.Message, exception);
                throw;
            }
        }

        private static BindingKernel Kernel => BindingKernel.Instance;
    }
}