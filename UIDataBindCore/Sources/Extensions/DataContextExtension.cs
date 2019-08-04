using System;
using UIDataBindCore.Properties;

namespace UIDataBindCore.Extensions
{
    public static class DataContextExtension
    {
        private static BindingKernel Kernel => BindingKernel.Instance;

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

        public static IBindProperty<TValue> FindProperty<TValue>(this IDataContext context, string memberName) =>
            FindMember(context, memberName, InternalFindProperty<TValue>);

        private static IBindProperty<TValue> InternalFindProperty<TValue>(this IDataContext context, string memberName) =>
            Kernel.ConversionMethods.AsPropertyOf<TValue> (Kernel.FindProperty(context, memberName))
            ?? new BindProperty<TValue>();

        public static Action FindMethod(this IDataContext context, string memberName) =>
            FindMember(context, memberName, InternalFindMethod);

        private static Action InternalFindMethod(this IDataContext context, string memberName) =>
            Kernel.FindMethod(context, memberName) ?? (() => { });

        private static TValue FindMember<TValue>(this IDataContext context, string memberName,
            Func<IDataContext, string, TValue> findMethod)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(memberName))
                throw new ArgumentException(nameof(memberName));

            try
            {
                return findMethod.Invoke(context, memberName);
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Source == nameof(BindingKernel))
                    throw new InvalidOperationException(exception.Message, exception);
                throw;
            }
        }
    }
}