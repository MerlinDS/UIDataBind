using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UIDataBindCore.Converters
{
    public class ConversionMethods : IConversionMethods
    {
        private readonly List<Tuple<Type, Type, Delegate>> _content;

        public ConversionMethods() =>
            _content = new List<Tuple<Type, Type, Delegate>>();

        public void Register<TType0, TType1>(Func<TType0, TType1> from0To1, Func<TType1, TType0> from1To0)
        {
            var type0 = typeof(TType0);
            var type1 = typeof(TType1);
            if(_content.Any(i=>Equals(i, type0, type1)))
                return;

            _content.Add(new Tuple<Type, Type, Delegate>(type0, type1, from0To1));
            _content.Add(new Tuple<Type, Type, Delegate>(type1, type0, from1To0));
        }

        public Func<TType0, TType1> Retrieve<TType0, TType1>()
        {
            var type0 = typeof(TType0);
            var type1 = typeof(TType1);
            return (Func<TType0, TType1>) _content.FirstOrDefault(i => Equals(i, type0, type1))?.Item3;
        }

        public void Dispose() => _content.Clear();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool Equals(Tuple<Type, Type, Delegate> item, Type type0, Type type1) =>
            item.Item1 == type0 && item.Item2 == type1;


    }
}