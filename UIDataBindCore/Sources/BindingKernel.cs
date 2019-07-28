using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UIDataBindCore.Base;
using UIDataBindCore.Extensions;

namespace UIDataBindCore
{
    /// <summary>
    /// The kernel of the UIDataBindCore library.
    /// </summary>
    public class BindingKernel
    {
        private readonly Dictionary<Guid, DataContextScope> _contextScopes;

        #region Singleton Access

        private static BindingKernel _instance;
        public static BindingKernel Instance => _instance ?? (_instance = new BindingKernel());

        private BindingKernel()
        {
            _contextScopes = new Dictionary<Guid, DataContextScope>();
        }

        #endregion


        #region PUBLIC API

        /// <summary></summary>
        /// <param name="context"></param>
        public void Register(IDataContext context)
        {
            var contextType = context.GetType();
            if (!HasScopeOf(contextType))
                RegisterScopeOf(contextType);

            //Maybe this action can be don't in other method (GetContextProperty etc.)
            TryAddContextInstance(context, contextType);
        }

        /// <summary></summary>
        /// <param name="context"></param>
        public void Unregister(IDataContext context)
        {
            var contextType = context.GetType();
            if (!HasScopeOf(contextType))
                throw new ArgumentException($"Scope of {contextType} was not registered yet!");

            var scope = GetScopeOf(contextType);
            scope.Remove(context);
            if (scope.Count != 0)
                return;

            //If there are no instances of context data in current scope, unregister it
            UnregisterScopeOf(contextType);
            scope.Dispose();
        }

        #endregion

        private void TryAddContextInstance(IDataContext context, Type contextType)
        {
            var scope = GetScopeOf(contextType);
            if (scope.Has(context))
                return;

            scope.Add(context);
            if (scope.Info.IsInitializable)
                (context as IInitializable)?.Init();
        }

        #region Scopes

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasScopeOf(Type contextType) =>
            _contextScopes.ContainsKey(contextType.GUID);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DataContextScope GetScopeOf(Type contextType) =>
            _contextScopes[contextType.GUID];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RegisterScopeOf(Type contextType) =>
            _contextScopes.Add(contextType.GUID, new DataContextScope(contextType.GetDataContextType()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnregisterScopeOf(Type contextType) =>
            _contextScopes.Remove(contextType.GUID);

        #endregion
    }
}