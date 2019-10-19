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

        public void Register<TSource, TValue>(Func<TSource, TValue> method)
        {
            var key = GetPairHashcode(typeof(TSource), typeof(TValue));
            if(_content.ContainsKey(key))
                return;

            _content.Add(key, method);
        }

        public TTarget Convert<TTarget>(object sourceValue)
        {
            var sourceType = sourceValue.GetType();
            var @delegate = Retrieve(sourceType, typeof(TTarget));
            return (TTarget) @delegate?.DynamicInvoke(sourceValue);
        }

        public object Convert<TSource>(Type targetType, TSource sourceValue)
        {
            var sourceType = typeof(TSource);
            var @delegate = Retrieve(sourceType, targetType);
            return @delegate?.DynamicInvoke(sourceValue);
        }

        public Delegate Retrieve(Type a, Type b)
        {
            Delegate method;
            return _content.TryGetValue( GetPairHashcode(a, b), out method) ? method : null;
        }

        public bool Has(Type a, Type b) =>
            _content.ContainsKey(GetPairHashcode(a, b));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetPairHashcode(Type a, Type b)=>
            ((a != null ? a.GetHashCode() : 0) * 397) ^ (b != null ? b.GetHashCode() : 0);
    }
}