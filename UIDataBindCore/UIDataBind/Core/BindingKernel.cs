using System;

namespace UIDataBindCore.Core
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

        ///<summary></summary>
        public void Register()
        {
            throw new NotImplementedException();
        }

        ///<summary></summary>
        public void Unregister()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}