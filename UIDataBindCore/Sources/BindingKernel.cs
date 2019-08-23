using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UIDataBindCore.Base;
using UIDataBindCore.Converters;
using UIDataBindCore.Extensions;

namespace UIDataBindCore
{
    /// <summary>
    /// The kernel of the UIDataBindCore library.
    /// </summary>
    public class BindingKernel : IDisposable
    {
        private readonly Dictionary<int, DataContextScope> _contextScopes;

        #region Singleton Access

        private static BindingKernel _instance;
        public static BindingKernel Instance => _instance ?? (_instance = new BindingKernel());

        private BindingKernel()
        {
            _contextScopes = new Dictionary<int, DataContextScope>();
            ConversionMethods = new ConversionMethods().RegisterBuildIn();
        }

        #endregion


        #region PUBLIC API

        public IConversionMethods ConversionMethods { get; }

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

        public IBindProperty FindProperty(IDataContext context, string memberName)
        {
            DataContextScope scope;
            return FindScopeOf(context, out scope) ? scope.FindProperty(context, memberName) : default;
        }

        public Action FindMethod(IDataContext context, string memberName)
        {
            DataContextScope scope;
            return FindScopeOf(context, out scope) ? scope.FindMethod(context, memberName) : default;
        }

        public IDataContext FindSubContext(IDataContext context, string memberName)
        {
            DataContextScope scope;
            return FindScopeOf(context, out scope) ? scope.FindSubContext(context, memberName) : default;
        }

        /// <summary></summary>
        /// <param name="context"></param>
        public void Unregister(IDataContext context)
        {
            var contextType = context.GetType();
            if (!HasScopeOf(contextType))
                throw new ArgumentException($"Scope of {contextType} was not registered yet!");

            var scope = GetScopeOf(contextType);
            if (!scope.Has(context))
                throw new ArgumentException($"{context} was not registered ins Scope yet!");

            scope.Remove(context);
            if (scope.Count > 0)
                return;

            //If there are no instances of context data in current scope, unregister it
            UnregisterScopeOf(contextType);
            scope.Dispose();
        }

        public void Dispose()
        {
            foreach (var scope in _contextScopes.Values)
                scope.Dispose();

            _contextScopes.Clear();
            _instance = null;
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

        private bool FindScopeOf(IDataContext context, out DataContextScope scope)
        {
            scope = default;
            var contextType = context.GetType();
            if (!HasScopeOf(contextType))
                return false;

            scope = GetScopeOf(contextType);
            if (!scope.Has(context))
                throw new InvalidOperationException("Context was not registered!") {Source = nameof(BindingKernel)};

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasScopeOf(Type contextType) =>
            _contextScopes.ContainsKey(contextType.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DataContextScope GetScopeOf(Type contextType) =>
            _contextScopes[contextType.GetHashCode()];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RegisterScopeOf(Type contextType) =>
            _contextScopes.Add(contextType.GetHashCode(), new DataContextScope(contextType.GetDataContextType()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnregisterScopeOf(Type contextType) =>
            _contextScopes.Remove(contextType.GetHashCode());

        #endregion
    }
}