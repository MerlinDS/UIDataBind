using System;
using System.Collections.Generic;
using UIDataBindCore.Extensions;

namespace UIDataBindCore.Base
{
    public struct DataContextScope : IDisposable
    {
        public int Count { get; private set; }
        public DataContextInfo Info { get; }

        private readonly Dictionary<int, DataContextReferences> _references;

        public DataContextScope(DataContextInfo info) : this()
        {
            Count = 0;
            Info = info;
            _references = new Dictionary<int, DataContextReferences>();
        }


        public bool Has(IDataContext instance) =>
            _references.ContainsKey(instance.GetHashCode());

        public void Add(IDataContext instance)
        {
            _references.Add(instance.GetHashCode(), instance.GetReferences(Info));
            Count++;
        }


        public void Remove(IDataContext instance)
        {
            _references.Remove(instance.GetHashCode());
            Count--;
        }

        public void Dispose()
        {
            _references.Clear();
            Count = 0;
        }
    }
}