using System;

namespace UIDataBind.Base
{
    /// <summary>
    /// Wrapper for binding path
    /// </summary>
    public struct BindingPath : IEquatable<BindingPath>, IComparable<BindingPath>,
        IEquatable<string>, IComparable<string>
    {
        private readonly string _value;

        private BindingPath(string value) => _value = value;

        public override string ToString() => $"Path[{_value}]";

        #region Oprators

        public static implicit operator string(BindingPath path) =>
            path._value;

        public static implicit operator BindingPath(string path) =>
            new BindingPath(path);

        public static bool operator ==(BindingPath a, BindingPath b) =>
            a.CompareTo(b) == 0;

        public static bool operator !=(BindingPath a, BindingPath b)=>
            a.CompareTo(b) != 0;

        public static bool operator ==(BindingPath a, string b) =>
            a.CompareTo(b) == 0;

        public static bool operator !=(BindingPath a, string b) =>
            a.CompareTo(b) != 0;


        #endregion

        public bool Equals(BindingPath other) =>
            Equals(other._value);

        public bool Equals(string other) =>
            _value == other;

        public override bool Equals(object obj) =>
            obj is BindingPath other && Equals(other);

        public override int GetHashCode() =>
            _value != null ? _value.GetHashCode() : 0;

        public int CompareTo(BindingPath other) =>
            CompareTo(other._value);

        public int CompareTo(string other) =>
            string.Compare(_value, other, StringComparison.Ordinal);
    }
}