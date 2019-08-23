using System;
using System.Collections.Generic;
using UIDataBindCore.Extensions;

namespace UIDataBindCore.Base
{
    public struct DataContextScope : IDisposable
    {
        private readonly Dictionary<int, DataContextReferences> _references;
        public DataContextInfo Info { get; }
        public int Count => _references.Count;

        public DataContextScope(DataContextInfo info)
        {
            Info = info;
            _references = new Dictionary<int, DataContextReferences>();
        }


        public bool Has(IDataContext instance) =>
            _references.ContainsKey(instance.GetHashCode());

        public void Add(IDataContext instance) =>
            _references.Add(instance.GetHashCode(), instance.GetReferences(Info));


        public void Remove(IDataContext instance) =>
            _references.Remove(instance.GetHashCode());

        public void Dispose() =>
            _references.Clear();

        public IBindProperty FindProperty(IDataContext instance, string memberName)
        {
            var references = _references[instance.GetHashCode()];
            return references.Properties.ContainsKey(memberName) ? references.Properties[memberName] : default;
        }

        public Action FindMethod(IDataContext instance, string memberName)
        {
            var references = _references[instance.GetHashCode()];
            return references.Methods.ContainsKey(memberName) ? references.Methods[memberName] : default;
        }

        public IDataContext FindSubContext(IDataContext instance, string memberName)
        {
            var references = _references[instance.GetHashCode()];
            return references.SubContexts.ContainsKey(memberName) ? references.SubContexts[memberName] : default;
        }
    }
}