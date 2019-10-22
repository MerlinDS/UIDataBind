using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using JetBrains.Annotations;

namespace UIDataBind.Editor.Tests
{
    [Serializable]
    [StructLayout(LayoutKind.Auto)]
    public struct BPath : IComparable, IComparable<BPath>, IEquatable<BPath>
    {
        public const int MaxLength = 11;
        public static readonly BPath Empty;
        private static readonly StringBuilder Sb = new StringBuilder();

        private readonly int _a;
        private readonly int _b;
        private readonly int _c;
        private readonly int _d;
        private readonly int _e;
        private readonly int _f;
        private readonly int _g;
        private readonly int _h;
        private readonly int _i;
        private readonly int _j;
        private readonly int _k;

        private readonly short _length;

        #region Constructors

        private BPath(int[] args)
        {
            _a = args.Length > 0 ? args[0] : 0;
            _b = args.Length > 1 ? args[1] : 0;
            _c = args.Length > 2 ? args[2] : 0;
            _d = args.Length > 3 ? args[3] : 0;
            _e = args.Length > 4 ? args[4] : 0;
            _f = args.Length > 5 ? args[5] : 0;
            _g = args.Length > 6 ? args[6] : 0;
            _h = args.Length > 7 ? args[7] : 0;
            _i = args.Length > 8 ? args[8] : 0;
            _j = args.Length > 9 ? args[9] : 0;
            _k = args.Length > 10 ? args[10] : 0;

            _length = (short) args.Length;
        }

        #endregion

        #region Public Static API

        public static BPath BuildFrom(params string[] args)
        {
            if(args.Length > MaxLength)
                ThrowOutOfRange();
            return new BPath(args.Select(ConvertToHash).ToArray());
        }

        public static BPath BuildFrom(BPath parent, params string[] args)
        {
            if (parent._length == 0)
                return BuildFrom(args);

            var length = parent.Lenght + args.Length;
            if(length > MaxLength)
                ThrowOutOfRange();

            var childArgs = new int[length];
            for (var i = 0; i < length; i++)
                childArgs[i] = i < parent.Lenght ? parent.GetElement(i) : ConvertToHash(args[i - parent.Lenght]);
            return new BPath(childArgs);
        }

        public static BPath GetParent(BPath path)
        {
            if (path._length == 0)
                return Empty;

            var args = new int[path._length - 1];
            for (var i = 0; i < args.Length; i++)
                args[i] = path.GetElement(i);
            return new BPath(args);
        }

        #endregion

        #region Public API

        public short Lenght => _length;

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _a;
                hashCode = (hashCode * 397) ^ _b;
                hashCode = (hashCode * 397) ^ _c;
                hashCode = (hashCode * 397) ^ _d;
                hashCode = (hashCode * 397) ^ _e;
                hashCode = (hashCode * 397) ^ _f;
                hashCode = (hashCode * 397) ^ _g;
                hashCode = (hashCode * 397) ^ _h;
                hashCode = (hashCode * 397) ^ _i;
                hashCode = (hashCode * 397) ^ _j;
                hashCode = (hashCode * 397) ^ _k;
                return hashCode;
            }
        }
        public override string ToString()
        {
            if (this == Empty)
                return "[Empty]";
            Sb.Clear();
            for (var i = 0; i < _length; i++)
            {
                if (i > 0)
                    Sb.Append('.');
                Sb.Append(ConvertToString(GetElement(i)));
            }

            return Sb.ToString();
        }

        #endregion

        #region Equals Methods

        public static bool operator ==(BPath a, BPath b) =>
            a.CompareTo(b) == 0;

        public static bool operator !=(BPath a, BPath b) =>
            a.CompareTo(b) != 0;

        public override bool Equals(object obj) =>
            obj is BPath other && Equals(other);

        public bool Equals(BPath other) =>
            CompareTo(other) == 0;

        #endregion

        #region Compare Methods

        public int CompareTo(object obj)
        {
            if (obj is BPath other)
                return CompareTo(other);
            return -1;
        }

        public int CompareTo(BPath other)
        {
            var lengthComparison = _length.CompareTo(other._length);
            if (lengthComparison != 0)
                return lengthComparison;
            var aComparison = _a.CompareTo(other._a);
            if (aComparison != 0)
                return aComparison;
            var bComparison = _b.CompareTo(other._b);
            if (bComparison != 0)
                return bComparison;
            var cComparison = _c.CompareTo(other._c);
            if (cComparison != 0)
                return cComparison;
            var dComparison = _d.CompareTo(other._d);
            if (dComparison != 0)
                return dComparison;
            var eComparison = _e.CompareTo(other._e);
            if (eComparison != 0)
                return eComparison;
            var fComparison = _f.CompareTo(other._f);
            if (fComparison != 0)
                return fComparison;
            var gComparison = _g.CompareTo(other._g);
            if (gComparison != 0)
                return gComparison;
            var hComparison = _h.CompareTo(other._h);
            if (hComparison != 0)
                return hComparison;
            var iComparison = _i.CompareTo(other._i);
            if (iComparison != 0)
                return iComparison;
            var jComparison = _j.CompareTo(other._j);
            if (jComparison != 0)
                return jComparison;
            return  _k.CompareTo(other._k);
        }

        #endregion

        #region Helpers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowOutOfRange() =>
            throw new ArgumentOutOfRangeException($"Can't build {nameof(BPath)} with args count more that BPath.MaxLength!");

        private int GetElement(int index)
        {
            switch (index)
            {
                case 0:
                    return _a;
                case 1:
                    return _b;
                case 2:
                    return _c;
                case 3:
                    return _d;
                case 4:
                    return _e;
                case 5:
                    return _f;
                case 6:
                    return _g;
                case 7:
                    return _h;
                case 8:
                    return _i;
                case 9:
                    return _j;
                case 10:
                    return _k;
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        #endregion

        #region Elements Converter

        private static readonly Dictionary<int, string> StringsMap = new Dictionary<int, string>();

        private static int ConvertToHash([NotNull] string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentException("source string could not be null or empty", nameof(source));

            var hashCode = source.GetHashCode();
            if (!StringsMap.ContainsKey(hashCode))
                StringsMap.Add(hashCode, source);
            return hashCode;
        }

        [NotNull]
        private static string ConvertToString(int hashCode)
        {
            if (hashCode == 0)
                throw new ArgumentException("hashCode of string could not be 0", nameof(hashCode));

            if (!StringsMap.ContainsKey(hashCode))
                throw new ArgumentException($"HashCode {hashCode} was not found", nameof(hashCode));
            return StringsMap[hashCode];
        }

        #endregion




    }
}