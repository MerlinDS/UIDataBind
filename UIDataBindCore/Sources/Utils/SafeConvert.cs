using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UIDataBindCore.Utils
{
    /// <summary>Converts a base data type to another base data type.</summary>
    public static class SafeConvert
    {
        #region Boolean

        public static bool ToBoolean(byte value) => Convert.ToBoolean(value);
        public static bool ToBoolean(int value)  => Convert.ToBoolean(value);

        public static bool ToBoolean(float value) => Convert.ToBoolean(value);

        public static bool ToBoolean(double value) => Convert.ToBoolean(value);

        public static bool ToBoolean(string value) =>
            FromString(value, false, true, Convert.ToBoolean);

        #endregion

        #region Byte

        public static byte ToByte(bool value) => Convert.ToByte(value);

        public static byte ToByte(int value) =>
            value < 0 ? byte.MinValue : value > byte.MaxValue ? byte.MaxValue : Convert.ToByte(value);

        public static byte ToByte(float value) => ToByte((double) value);

        public static byte ToByte(double value) =>
            value < 0 ? byte.MinValue : value > byte.MaxValue ? byte.MaxValue : Convert.ToByte(value);

        public static byte ToByte(string value) =>
            FromString(value, (byte) 0, byte.MaxValue, Convert.ToByte);

        #endregion

        #region Int32

        public static int ToInt32(bool value)  => Convert.ToInt32(value);
        public static int ToInt32(byte value)  => Convert.ToInt32(value);
        public static int ToInt32(float value) => ToInt32((double) value);

        public static int ToInt32(double value) =>
            value >= int.MaxValue ? int.MaxValue : value <= int.MinValue ? int.MinValue : Convert.ToInt32(value);

        public static int ToInt32(string value) =>
            FromString(value, 0, int.MaxValue, Convert.ToInt32);

        #endregion

        #region Float

        public static float ToSingle(bool value)   => Convert.ToSingle(value);
        public static float ToSingle(byte value)   => Convert.ToSingle(value);
        public static float ToSingle(int value)    => Convert.ToSingle(value);
        public static float ToSingle(double value) => Convert.ToSingle(value);
        public static float ToSingle(string value) =>
            FromString(value, 0, float.MaxValue, Convert.ToSingle);

        #endregion

        #region Double

        public static double ToDouble(bool value)   => Convert.ToDouble(value);
        public static double ToDouble(byte value)   => Convert.ToDouble(value);
        public static double ToDouble(int value)    => Convert.ToDouble(value);
        public static double ToDouble(float value)  => Convert.ToDouble(value);
        public static double ToDouble(string value) =>
            FromString(value, 0, double.MaxValue, Convert.ToDouble);

        #endregion

        #region String

        public static string ToString(bool value)   => Convert.ToString(value);
        public static string ToString(byte value)   => Convert.ToString(value);
        public static string ToString(int value)    => Convert.ToString(value);
        public static string ToString(float value)  => Convert.ToString(value, CultureInfo.CurrentCulture);

        public static string ToString(double value) => Convert.ToString(value, CultureInfo.CurrentCulture);

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TValue FromString<TValue>(string value, TValue @default, TValue overflow,
            Func<string, TValue> convert)
        {
            try
            {
                return convert.Invoke(value);
            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    case FormatException _:
                        return @default;
                    case OverflowException _:
                        return overflow;
                    default:
                        throw;
                }
            }
        }
    }
}