using System;
using UIDataBindCore;
using UIDataBindCore.Extensions;

namespace Tests.UIDataBindCore
{
    /// <summary>
    /// The example of initialization and binding <see cref="IBinder"/> to <see cref="IDataContext"/> method.
    /// <remarks>
    /// Only methods without arguments and the return type could be bonded.
    /// </remarks>
    /// </summary>
    public class SampleMethodBinder : IBinder
    {
        private readonly string _path;
        private Action _method;

        /// <summary>
        /// Accessor to a method
        /// </summary>
        public Action Method => _method;
        public bool HasMethod=> _method != null;

        public IDataContext Context { get; }
        public SampleMethodBinder(IDataContext context, string path)
        {
            _path = path;
            Context = context;
        }

        /// <summary>
        /// Finding of a method in a <see cref="IDataContext"/> and save it to binder
        /// </summary>
        public void Bind() => _method = Context.FindMethod(_path);

        /// <summary>
        /// Remove a method from binder
        /// </summary>
        public void Unbind() => _method = null;

        /// <summary>
        /// Will invokes bonded method
        /// </summary>
        public void Invoke() => _method.Invoke();
    }
}