using System;
using System.Runtime.CompilerServices;

namespace UIDataBindCore.Properties.Adapters
{
    /// <summary>
    /// Available types for conversion
    /// </summary>
    internal static class ConvertibleTypes
    {
        private static readonly Type BooleanType = typeof(bool);
        private static readonly Type ByteType = typeof(byte);
        private static readonly Type Int32Type = typeof(int);
        private static readonly Type FloatType = typeof(float);
        private static readonly Type DoubleType = typeof(double);
        private static readonly Type DecimalType = typeof(decimal);
        private static readonly Type StringType = typeof(string);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBoolean(this Type type) => type == BooleanType;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsByte(this Type type) => type == ByteType;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInt32(this Type type) => type == Int32Type;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFloat(this Type type) => type == FloatType;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDouble(this Type type) => type == DoubleType;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDecimal(this Type type) => type == DecimalType;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsString(this Type type) => type == StringType;
    }
}