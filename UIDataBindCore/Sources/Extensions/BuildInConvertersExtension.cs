using System;
using UIDataBindCore.Converters;

namespace UIDataBindCore.Extensions
{
    public static class BuildInConvertersExtension
    {
        public static IConversionMethods RegisterBuildIn(this IConversionMethods conversionMethods)
        {
            if (conversionMethods == null)
                throw new ArgumentNullException(nameof(conversionMethods));

            conversionMethods.RegisterBoolean();
            conversionMethods.RegisterByte();
            conversionMethods.RegisterInt32();
            conversionMethods.RegisterSingle();
            conversionMethods.RegisterDouble();
            conversionMethods.RegisterString();

            return conversionMethods;
        }

        private static void RegisterBoolean(this IConversionMethods conversionMethods)
        {
            conversionMethods.Register<bool, byte>(SafeConvert.ToBoolean, SafeConvert.ToByte);
            conversionMethods.Register<bool, int>(SafeConvert.ToBoolean, SafeConvert.ToInt32);
            conversionMethods.Register<bool, float>(SafeConvert.ToBoolean, SafeConvert.ToSingle);
            conversionMethods.Register<bool, double>(SafeConvert.ToBoolean, SafeConvert.ToDouble);
            conversionMethods.Register<bool, string>(SafeConvert.ToBoolean, SafeConvert.ToString);
        }

        private static void RegisterByte(this IConversionMethods conversionMethods)
        {
            conversionMethods.Register<byte, bool>(SafeConvert.ToByte, SafeConvert.ToBoolean);
            conversionMethods.Register<byte, int>(SafeConvert.ToByte, SafeConvert.ToInt32);
            conversionMethods.Register<byte, float>(SafeConvert.ToByte, SafeConvert.ToSingle);
            conversionMethods.Register<byte, double>(SafeConvert.ToByte, SafeConvert.ToDouble);
            conversionMethods.Register<byte, string>(SafeConvert.ToByte, SafeConvert.ToString);
        }

        private static void RegisterInt32(this IConversionMethods conversionMethods)
        {
            conversionMethods.Register<int, bool>(SafeConvert.ToInt32, SafeConvert.ToBoolean);
            conversionMethods.Register<int, byte>(SafeConvert.ToInt32, SafeConvert.ToByte);
            conversionMethods.Register<int, float>(SafeConvert.ToInt32, SafeConvert.ToSingle);
            conversionMethods.Register<int, double>(SafeConvert.ToInt32, SafeConvert.ToDouble);
            conversionMethods.Register<int, string>(SafeConvert.ToInt32, SafeConvert.ToString);
        }

        private static void RegisterSingle(this IConversionMethods conversionMethods)
        {
            conversionMethods.Register<float, bool>(SafeConvert.ToSingle, SafeConvert.ToBoolean);
            conversionMethods.Register<float, byte>(SafeConvert.ToSingle, SafeConvert.ToByte);
            conversionMethods.Register<float, int>(SafeConvert.ToSingle, SafeConvert.ToInt32);
            conversionMethods.Register<float, double>(SafeConvert.ToSingle, SafeConvert.ToDouble);
            conversionMethods.Register<float, string>(SafeConvert.ToSingle, SafeConvert.ToString);
        }

        private static void RegisterDouble(this IConversionMethods conversionMethods)
        {
            conversionMethods.Register<double, bool>(SafeConvert.ToDouble, SafeConvert.ToBoolean);
            conversionMethods.Register<double, byte>(SafeConvert.ToDouble, SafeConvert.ToByte);
            conversionMethods.Register<double, int>(SafeConvert.ToDouble, SafeConvert.ToInt32);
            conversionMethods.Register<double, float>(SafeConvert.ToDouble, SafeConvert.ToSingle);
            conversionMethods.Register<double, string>(SafeConvert.ToDouble, SafeConvert.ToString);
        }

        private static void RegisterString(this IConversionMethods conversionMethods)
        {
            conversionMethods.Register<string, bool>(SafeConvert.ToString, SafeConvert.ToBoolean);
            conversionMethods.Register<string, byte>(SafeConvert.ToString, SafeConvert.ToByte);
            conversionMethods.Register<string, int>(SafeConvert.ToString, SafeConvert.ToInt32);
            conversionMethods.Register<string, float>(SafeConvert.ToString, SafeConvert.ToSingle);
            conversionMethods.Register<string, double>(SafeConvert.ToString, SafeConvert.ToDouble);
        }
    }
}