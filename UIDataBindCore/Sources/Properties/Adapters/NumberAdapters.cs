using System;
using System.Globalization;

namespace UIDataBindCore.Properties.Adapters
{
    public static class NumberAdapters
    {
        public static IBindProperty<TTarget> To<TTarget>(this IBindProperty<byte> source)
        {
            var targetType = typeof(TTarget);

            if (targetType.IsBoolean())
                return (IBindProperty<TTarget>) BindPropertyAdapter<byte, bool>.Instantiate(source, Convert.ToByte, Convert.ToBoolean);
            if (targetType.IsByte())
                return (IBindProperty<TTarget>) source;
            if (targetType.IsInt32())
                return (IBindProperty<TTarget>) BindPropertyAdapter<byte, int>.Instantiate(source, Convert.ToByte, Convert.ToInt32);
            if (targetType.IsFloat())
                return (IBindProperty<TTarget>) BindPropertyAdapter<byte, float>.Instantiate(source, Convert.ToByte, Convert.ToSingle);
            if (targetType.IsDouble())
                return (IBindProperty<TTarget>) BindPropertyAdapter<byte, double>.Instantiate(source, Convert.ToByte, Convert.ToDouble);
            if (targetType.IsDecimal())
                return (IBindProperty<TTarget>) BindPropertyAdapter<byte, decimal>.Instantiate(source, Convert.ToByte, Convert.ToDecimal);
            if (targetType.IsString())
                return (IBindProperty<TTarget>) BindPropertyAdapter<byte, string>.Instantiate(source, byte.Parse, Convert.ToString);

            throw new FormatException($"Can't convert {source} to {typeof(IBindProperty<TTarget>)}");
        }

        public static IBindProperty<TTarget> To<TTarget>(this IBindProperty<int> source)
        {
            var targetType = typeof(TTarget);

            if (targetType.IsBoolean())
                return (IBindProperty<TTarget>) BindPropertyAdapter<int, bool>.Instantiate(source, Convert.ToInt32, Convert.ToBoolean);
            if (targetType.IsByte())
                return (IBindProperty<TTarget>) BindPropertyAdapter<int, byte>.Instantiate(source, Convert.ToInt32, Convert.ToByte);
            if (targetType.IsInt32())
                return (IBindProperty<TTarget>) source;
            if (targetType.IsFloat())
                return (IBindProperty<TTarget>) BindPropertyAdapter<int, float>.Instantiate(source, Convert.ToInt32, Convert.ToSingle);
            if (targetType.IsDouble())
                return (IBindProperty<TTarget>) BindPropertyAdapter<int, double>.Instantiate(source, Convert.ToInt32, Convert.ToDouble);
            if (targetType.IsDecimal())
                return (IBindProperty<TTarget>) BindPropertyAdapter<int, decimal>.Instantiate(source, Convert.ToInt32, Convert.ToDecimal);
            if (targetType.IsString())
                return (IBindProperty<TTarget>) BindPropertyAdapter<int, string>.Instantiate(source, int.Parse, Convert.ToString);

            throw new FormatException($"Can't convert {source} to {typeof(IBindProperty<TTarget>)}");
        }

        public static IBindProperty<TTarget> To<TTarget>(this IBindProperty<float> source)
        {
            var targetType = typeof(TTarget);

            if (targetType.IsBoolean())
                return (IBindProperty<TTarget>) BindPropertyAdapter<float, bool>.Instantiate(source, Convert.ToSingle, Convert.ToBoolean);
            if (targetType.IsByte())
                return (IBindProperty<TTarget>) BindPropertyAdapter<float, byte>.Instantiate(source, Convert.ToSingle, Convert.ToByte);
            if (targetType.IsInt32())
                return (IBindProperty<TTarget>) BindPropertyAdapter<float, int>.Instantiate(source, Convert.ToSingle, Convert.ToInt32);
            if (targetType.IsFloat())
                return (IBindProperty<TTarget>) source;
            if (targetType.IsDouble())
                return (IBindProperty<TTarget>) BindPropertyAdapter<float, double>.Instantiate(source, Convert.ToSingle, Convert.ToDouble);
            if (targetType.IsDecimal())
                return (IBindProperty<TTarget>) BindPropertyAdapter<float, decimal>.Instantiate(source, Convert.ToSingle, Convert.ToDecimal);
            if (targetType.IsString())
                return (IBindProperty<TTarget>) BindPropertyAdapter<float, string>.Instantiate(source, float.Parse, Convert.ToString);

            throw new FormatException($"Can't convert {source} to {typeof(IBindProperty<TTarget>)}");
        }

        public static IBindProperty<TTarget> To<TTarget>(this IBindProperty<double> source)
        {
            var targetType = typeof(TTarget);

            if (targetType.IsBoolean())
                return (IBindProperty<TTarget>) BindPropertyAdapter<double, bool>.Instantiate(source, Convert.ToDouble, Convert.ToBoolean);
            if (targetType.IsByte())
                return (IBindProperty<TTarget>) BindPropertyAdapter<double, byte>.Instantiate(source, Convert.ToDouble, Convert.ToByte);
            if (targetType.IsInt32())
                return (IBindProperty<TTarget>) BindPropertyAdapter<double, int>.Instantiate(source, Convert.ToDouble, Convert.ToInt32);
            if (targetType.IsFloat())
                return (IBindProperty<TTarget>) BindPropertyAdapter<double, float>.Instantiate(source, Convert.ToDouble, Convert.ToSingle);
            if (targetType.IsDouble())
                return (IBindProperty<TTarget>) source;
            if (targetType.IsDecimal())
                return (IBindProperty<TTarget>) BindPropertyAdapter<double, decimal>.Instantiate(source, Convert.ToDouble, Convert.ToDecimal);
            if (targetType.IsString())
                return (IBindProperty<TTarget>) BindPropertyAdapter<double, string>.Instantiate(source, double.Parse, Convert.ToString);

            throw new FormatException($"Can't convert {source} to {typeof(IBindProperty<TTarget>)}");
        }

        public static IBindProperty<TTarget> To<TTarget>(this IBindProperty<decimal> source)
        {
            var targetType = typeof(TTarget);

            if (targetType.IsBoolean())
                return (IBindProperty<TTarget>) BindPropertyAdapter<decimal, bool>.Instantiate(source, Convert.ToDecimal, Convert.ToBoolean);
            if (targetType.IsByte())
                return (IBindProperty<TTarget>) BindPropertyAdapter<decimal, byte>.Instantiate(source, Convert.ToDecimal, Convert.ToByte);
            if (targetType.IsInt32())
                return (IBindProperty<TTarget>) BindPropertyAdapter<decimal, int>.Instantiate(source, Convert.ToDecimal, Convert.ToInt32);
            if (targetType.IsFloat())
                return (IBindProperty<TTarget>) BindPropertyAdapter<decimal, float>.Instantiate(source, Convert.ToDecimal, Convert.ToSingle);
            if (targetType.IsDouble())
                return (IBindProperty<TTarget>) BindPropertyAdapter<decimal, double>.Instantiate(source, Convert.ToDecimal, Convert.ToDouble);
            if (targetType.IsDecimal())
                return (IBindProperty<TTarget>) source;
            if (targetType.IsString())
                return (IBindProperty<TTarget>) BindPropertyAdapter<decimal, string>.Instantiate(source, decimal.Parse, Convert.ToString);

            throw new FormatException($"Can't convert {source} to {typeof(IBindProperty<TTarget>)}");
        }
    }
}