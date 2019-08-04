using System;
using UIDataBindCore;
using UIDataBindCore.Extensions;

namespace Tests.UIDataBindCore
{
    public class SampleMethodBinder : IBinder
    {
        private readonly string _path;
        private Action _method;

        public bool HasMethod=> _method != null;

        public void Invoke() => _method.Invoke();

        public SampleMethodBinder(string path) => _path = path;

        public void Bind(IDataContext context) => _method = context.FindMethod(_path);

        public void Unbind() => _method = null;

        public Action Method => _method;
    }
}