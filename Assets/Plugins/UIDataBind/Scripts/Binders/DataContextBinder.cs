using UIDataBindCore;
using UIDataBindCore.Extensions;

namespace Plugins.UIDataBind.Binders
{
    public class DataContextBinder<TContext> : DataContextBinder
        where TContext : class, IDataContext, new()
    {
        protected override IDataContext GetContextInstance() => new TContext();
    }

    public abstract class DataContextBinder : AbstractBinder, IDataContextBinder
    {
        private bool _bound;
        private IDataContext _context;
        public sealed override IDataContext Context
        {
            get
            {
                if (_context != null)
                    return _context;

                _context = GetContextInstance();
                Bind();
                return _context;
            }
        }

        protected abstract IDataContext GetContextInstance();

        #region Bindings

        /// <inheritdoc/>
        public override void Bind()
        {
            if(_bound)
                return;

            Context.Register();
            _bound = true;
        }

        /// <inheritdoc/>
        public override void Unbind()
        {
            if(!_bound || _context == null)
                return;

            Context.Unregister();
            _bound = false;
        }

        #endregion

    }
}