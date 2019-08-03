using System;

namespace UIDataBindCore.Base
{
    [Serializable]
    public struct TypesPair
    {
        private readonly Type _a;
        private readonly Type _b;

        public static TypesPair Create<T1, T2>()
            => new TypesPair(typeof(T1), typeof(T2));
        public TypesPair(Type a, Type b)
        {
            _a = a;
            _b = b;
        }

        public Type A => _a;

        public Type B => _b;

        public bool Equals(TypesPair other) =>
            _a == other._a && _b == other._b;

        public override bool Equals(object obj) =>
            obj is TypesPair other && Equals(other);

        public bool Equals<T1, T2>() =>
            this.Equals(typeof(T1), typeof(T2));
        public bool Equals(Type a, Type b) =>
            _a == a && _b == b;

        public override int GetHashCode() =>
            ((_a != null ? _a.GetHashCode() : 0) * 397) ^ (_b != null ? _b.GetHashCode() : 0);

        public override string ToString() =>
            $"{nameof(TypesPair)}[{_a} {_b}]";
    }
}