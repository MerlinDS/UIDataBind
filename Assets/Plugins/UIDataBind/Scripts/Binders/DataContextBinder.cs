using System;
using UIDataBindCore;
using UIDataBindCore.Extensions;
using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    public class DataContextBinder<TContext> : DataContextBinder
        where TContext : class, IDataContext, new()
    {
        protected override IDataContext GetContextInstance() => new TContext();

        protected new TContext Context => (TContext) base.Context;
    }

    public abstract class DataContextBinder : AbstractBinder, IDataContextBinder
    {
        private bool _bound;
        private IDataContext _context;

        public sealed override IDataContext Context
        {
            get
            {
                // ReSharper disable once InvertIf
                if (_context == null)
                {
                    _context = GetContextInstance();
                    Configure();

                    if (!_bound && Application.isPlaying)
                        Bind();
                }

                return _context;
            }
        }

        /// <summary>
        /// Get type of a concrete <see cref="IDataContext"/>
        /// </summary>
        public Type ContextType => GetContextInstance()?.GetType();
        /// <summary>
        /// Get instance of a concrete <see cref="IDataContext"/>
        /// </summary>
        /// <returns>Instance of a concrete <see cref="IDataContext"/></returns>
        protected abstract IDataContext GetContextInstance();
        /// <summary>
        /// Configure <see cref="IDataContext"/> before it's initialization
        /// </summary>
        protected virtual void Configure(){}

        #region Bindings

        /// <inheritdoc/>
        public sealed override void Bind()
        {
            if (_bound)
                return;

            Context.Register();
            _bound = true;
        }

        /// <inheritdoc/>
        public sealed override void Unbind()
        {
            if (!_bound)
                return;

            _context?.Unregister();
            _context = null;
            _bound = false;
        }

        #endregion
    }
}