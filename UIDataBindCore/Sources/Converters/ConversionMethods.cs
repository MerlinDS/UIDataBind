using System;
using System.Collections.Generic;
using System.Linq;
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
            var type0 = typeof(TType0);
            var type1 = typeof(TType1);
            if(Has(type0, type1))
                return;

            Add(type0, type1, from0To1);
            Add(type1, type0, from1To0);
        }

        private void Add(Type type0, Type type1, Delegate @delegate)
        {
            _keys.Add(new TypesPair(type0, type1));
            _content.Add(@delegate);
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


    }
}