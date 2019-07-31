using System;
using UIDataBindCore.Properties;
using UIDataBindCore.Utils;

namespace UIDataBindCore.Extensions
{
    public static partial class AdapterExtension
    {
        public static IBindProperty<TTarget> To<TTarget>(this IBindProperty<bool> source)
        {
            var typeCode = Type.GetTypeCode(typeof(TTarget));
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return (IBindProperty<TTarget>) source;
                case TypeCode.Byte:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<bool, byte>
                        (source, SafeConvert.ToBoolean, SafeConvert.ToByte);
                case TypeCode.Int32:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<bool, int>
                        (source, SafeConvert.ToBoolean, SafeConvert.ToInt32);
                case TypeCode.Single:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<bool, float>
                        (source, SafeConvert.ToBoolean, SafeConvert.ToSingle);
                case TypeCode.Double:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<bool, double>
                        (source, SafeConvert.ToBoolean, SafeConvert.ToDouble);
                case TypeCode.String:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<bool, string>
                        (source, SafeConvert.ToBoolean, SafeConvert.ToString);
                default:
                    throw new FormatException($"Can't convert {source} to {typeof(IBindProperty<TTarget>)}");
            }
        }

        public static IBindProperty<TTarget> To<TTarget>(this IBindProperty<byte> source)
        {
            var typeCode = Type.GetTypeCode(typeof(TTarget));
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<byte, bool>
                        (source, SafeConvert.ToByte, SafeConvert.ToBoolean);
                case TypeCode.Byte:
                    return (IBindProperty<TTarget>) source;
                case TypeCode.Int32:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<byte, int>
                        (source, SafeConvert.ToByte, SafeConvert.ToInt32);
                case TypeCode.Single:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<byte, float>
                        (source, SafeConvert.ToByte, SafeConvert.ToSingle);
                case TypeCode.Double:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<byte, double>
                        (source, SafeConvert.ToByte, SafeConvert.ToDouble);
                case TypeCode.String:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<byte, string>
                        (source, SafeConvert.ToByte, SafeConvert.ToString);
                default:
                    throw new FormatException($"Can't convert {source} to {typeof(IBindProperty<TTarget>)}");
            }
        }

        public static IBindProperty<TTarget> To<TTarget>(this IBindProperty<int> source)
        {
            var typeCode = Type.GetTypeCode(typeof(TTarget));
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<int, bool>
                        (source, SafeConvert.ToInt32, SafeConvert.ToBoolean);
                case TypeCode.Byte:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<int, byte>
                        (source, SafeConvert.ToInt32, SafeConvert.ToByte);
                case TypeCode.Int32:
                    return (IBindProperty<TTarget>) source;
                case TypeCode.Single:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<int, float>
                        (source, SafeConvert.ToInt32, SafeConvert.ToSingle);
                case TypeCode.Double:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<int, double>
                        (source, SafeConvert.ToInt32, SafeConvert.ToDouble);
                case TypeCode.String:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<int, string>
                        (source, SafeConvert.ToInt32, SafeConvert.ToString);
                default:
                    throw new FormatException($"Can't convert {source} to {typeof(IBindProperty<TTarget>)}");
            }

        }

        public static IBindProperty<TTarget> To<TTarget>(this IBindProperty<float> source)
        {
            var typeCode = Type.GetTypeCode(typeof(TTarget));
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<float, bool>
                        (source, SafeConvert.ToSingle, SafeConvert.ToBoolean);
                case TypeCode.Byte:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<float, byte>
                        (source, SafeConvert.ToSingle, SafeConvert.ToByte);
                case TypeCode.Int32:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<float, int>
                        (source, SafeConvert.ToSingle, SafeConvert.ToInt32);
                case TypeCode.Single:
                    return (IBindProperty<TTarget>) source;
                case TypeCode.Double:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<float, double>
                        (source, SafeConvert.ToSingle, SafeConvert.ToDouble);
                case TypeCode.String:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<float, string>
                        (source, SafeConvert.ToSingle, SafeConvert.ToString);
                default:
                    throw new FormatException($"Can't convert {source} to {typeof(IBindProperty<TTarget>)}");
            }
        }

        public static IBindProperty<TTarget> To<TTarget>(this IBindProperty<double> source)
        {
            var typeCode = Type.GetTypeCode(typeof(TTarget));
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<double, bool>
                        (source, SafeConvert.ToDouble, SafeConvert.ToBoolean);
                case TypeCode.Byte:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<double, byte>
                        (source, SafeConvert.ToDouble, SafeConvert.ToByte);
                case TypeCode.Int32:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<double, int>
                        (source, SafeConvert.ToDouble, SafeConvert.ToInt32);
                case TypeCode.Single:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<double, float>
                        (source, SafeConvert.ToDouble, SafeConvert.ToSingle);
                case TypeCode.Double:
                    return (IBindProperty<TTarget>) source;
                case TypeCode.String:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<double, string>
                        (source, SafeConvert.ToDouble, SafeConvert.ToString);
                default:
                    throw new FormatException($"Can't convert {source} to {typeof(IBindProperty<TTarget>)}");
            }
        }

        public static IBindProperty<TTarget> To<TTarget>(this IBindProperty<string> source)
        {
            var typeCode = Type.GetTypeCode(typeof(TTarget));
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<string, bool>
                        (source, SafeConvert.ToString, SafeConvert.ToBoolean);
                case TypeCode.Byte:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<string, byte>
                        (source, SafeConvert.ToString, SafeConvert.ToByte);
                case TypeCode.Int32:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<string, int>
                        (source, SafeConvert.ToString, SafeConvert.ToInt32);
                case TypeCode.Single:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<string, float>
                        (source, SafeConvert.ToString, SafeConvert.ToSingle);
                case TypeCode.Double:
                    return (IBindProperty<TTarget>) new BindPropertyAdapter<string, double>
                        (source, SafeConvert.ToString, SafeConvert.ToDouble);
                case TypeCode.String:
                    return (IBindProperty<TTarget>) source;
                default:
                    throw new FormatException($"Can't convert {source} to {typeof(IBindProperty<TTarget>)}");
            }
        }
    }
}