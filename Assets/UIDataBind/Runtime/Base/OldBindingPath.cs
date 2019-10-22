using System;

namespace UIDataBind.Base
{
    [Obsolete("Use BindingPath instead")]
    public struct OldBindingPath : IEquatable<OldBindingPath>, IComparable<OldBindingPath>,
        IEquatable<string>, IComparable<string>
    {
        private readonly string _value;

        private OldBindingPath(string value) => _value = value;

        public bool IsEmpty => string.IsNullOrEmpty(_value);

        public override string ToString() => $"Path[{_value}]";

        #region Oprators

        public static implicit operator BindingPath(OldBindingPath path) =>
            BindingPath.BuildFrom((string)path);

        public static implicit operator OldBindingPath(BindingPath path) =>
            path.ToString();

        public static implicit operator string(OldBindingPath path) =>
            path._value;

        public static implicit operator OldBindingPath(string path) =>
            new OldBindingPath(path);

        public static bool operator ==(OldBindingPath a, OldBindingPath b) =>
            a.CompareTo(b) == 0;

        public static bool operator !=(OldBindingPath a, OldBindingPath b)=>
            a.CompareTo(b) != 0;

        public static bool operator ==(OldBindingPath a, string b) =>
            a.CompareTo(b) == 0;

        public static bool operator !=(OldBindingPath a, string b) =>
            a.CompareTo(b) != 0;


        #endregion

        public bool Equals(OldBindingPath other) =>
            Equals(other._value);

        public bool Equals(string other) =>
            _value == other;

        public override bool Equals(object obj) =>
            obj is OldBindingPath other && Equals(other);

        public override int GetHashCode() =>
            _value != null ? _value.GetHashCode() : 0;

        public int CompareTo(OldBindingPath other) =>
            CompareTo(other._value);

        public int CompareTo(string other) =>
            string.Compare(_value, other, StringComparison.Ordinal);
    }
}