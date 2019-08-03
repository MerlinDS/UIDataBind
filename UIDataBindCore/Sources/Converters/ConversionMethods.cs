using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UIDataBindCore.Base;

namespace UIDataBindCore.Converters
{
    public class ConversionMethods : IConversionMethods
    {
        private readonly List<TypesPair> _keys;
        private readonly List<Delegate> _content;

        public ConversionMethods()
        {
            _keys = new List<TypesPair>();
            _content = new List<Delegate>();
        }

        public void Register<TType0, TType1>(Func<TType1, TType0> from1To0, Func<TType0, TType1> from0To1)
        {
            if(Has<TType0, TType1>())
                return;

            Add<TType0, TType1>(from0To1);
            Add<TType1, TType0>(from1To0);
        }

        public bool Has(Type type0, Type type1) =>
            _keys.Any(k=>k.Equals(type0, type1));

        public Delegate Retrieve(Type type0, Type type1)
        {
            var index = _keys.FindIndex(k => k.Equals(type0, type1));
            if (index < 0)
                throw new ArgumentException(
                    $"A conversion method with {nameof(TypesPair)}{type0}{type0} was not registered!");
            return _content[index];
        }

        public void Dispose()
        {
            _content.Clear();
            _keys.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Add<TType0, TType1>(Delegate @delegate)
        {
            _keys.Add(TypesPair.Create<TType0, TType1>());
            _content.Add(@delegate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool Has<TType0, TType1>() =>
            _keys.Any(k=>k.Equals<TType0, TType1>());

    }
}