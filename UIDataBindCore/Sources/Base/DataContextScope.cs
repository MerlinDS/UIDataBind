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

        public DataContextScope(DataContextInfo info) : this()
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
    }
}