using System;
using System.Collections.Generic;

namespace UIDataBindCore.Base
{
    public struct DataContextReferences
    {
        public readonly IDataContext Instance;
        public readonly Dictionary<string, Action> Methods;
        public readonly Dictionary<string, IBindProperty> Properties;

        public DataContextReferences(IDataContext instance)
        {
            Instance = instance;
            Methods = new Dictionary<string, Action>();
            Properties = new Dictionary<string, IBindProperty>();
        }
    }
}