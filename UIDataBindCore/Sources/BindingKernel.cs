using System;

namespace UIDataBindCore
{
    /// <summary>
    /// The kernel of the UIDataBindCore library.
    /// </summary>
    public class BindingKernel
    {
        private static BindingKernel _instance;
        public static BindingKernel Instance => _instance ?? (_instance = new BindingKernel());

        private BindingKernel()
        {

        }

        #region PUBLIC API

        /// <summary></summary>
        /// <param name="context"></param>
        public void Register(IDataContext context)
        {
            //TODO: Implement creation of the scope of bindings (field reflections)
            throw new NotImplementedException();
        }

        /// <summary></summary>
        /// <param name="context"></param>
        public void Unregister(IDataContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}