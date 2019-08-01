using System;
using System.Collections.Generic;
using System.Linq;

namespace UIDataBindCore.Converters
{
    public class ConvertersCollection : IDisposable
    {
        private readonly ICollection<Tuple<Type, Type, IPropertyConverter>> _converters;

        public ConvertersCollection() =>
            _converters = new List<Tuple<Type, Type, IPropertyConverter>>();

        public void Register<TValue0, TValue1>(IPropertyConverter<TValue0, TValue1> propertyConverter)
        {
            if (propertyConverter == null)
                throw new ArgumentNullException(nameof(propertyConverter));

            if (_converters.Any(c => c.Item3 == propertyConverter))
                return;

            _converters.Add(new Tuple<Type, Type, IPropertyConverter>(typeof(TValue0), typeof(TValue1), propertyConverter));
        }

        public IPropertyConverter Retrieve<TValue>(Type sourceType) =>
            _converters.FirstOrDefault(c => IsConvertible(c, sourceType, typeof(TValue)))?.Item3;

        public void Dispose() => _converters.Clear();

        private static bool IsConvertible(Tuple<Type, Type, IPropertyConverter> c, Type sourceType, Type targetType) =>
            c.Item1 == sourceType && c.Item2 == targetType || c.Item2 == sourceType && c.Item1 == targetType;

    }
}