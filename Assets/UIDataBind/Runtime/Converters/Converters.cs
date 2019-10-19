using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UIDataBind.Converters
{
    public sealed class Converters : IConverters
    {
        private readonly Dictionary<int, Delegate> _content;

        public Converters() =>
            _content = new Dictionary<int, Delegate>();

        public bool Has(Type a, Type b) =>
            _content.ContainsKey(GetPairHashcode(a, b));

        public void Register<TSource, TValue>(Func<TSource, TValue> method)
        {
            var key = GetPairHashcode(typeof(TSource), typeof(TValue));
            if(!_content.ContainsKey(key))
                _content.Add(key, method);
        }

        public TTarget Convert<TTarget>(object value) =>
            (TTarget) Convert(value.GetType(), typeof(TTarget), value);

        public object Convert<TSource>(Type targetType, TSource value) =>
            Convert(typeof(TSource), targetType, value);

        private object Convert(Type sourceType, Type targetType, object value) =>
            Retrieve(sourceType, targetType)?.DynamicInvoke(value);

        private Delegate Retrieve(Type a, Type b)
        {
            Delegate method;
            return _content.TryGetValue( GetPairHashcode(a, b), out method) ? method : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetPairHashcode(Type a, Type b)=>
            ((a != null ? a.GetHashCode() : 0) * 397) ^ (b != null ? b.GetHashCode() : 0);
    }
}