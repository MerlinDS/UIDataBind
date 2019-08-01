using UIDataBindCore.Utils;

namespace UIDataBindCore.Converters
{
    public class DoubleToBooleanConverter : IPropertyConverter<double, bool>
    {
        public double Convert(bool value)   => SafeConvert.ToDouble(value);
        public bool   Convert(double value) => SafeConvert.ToBoolean(value);
    }

    public class DoubleToByteConverter : IPropertyConverter<double, byte>
    {
        public double Convert(byte value)   => SafeConvert.ToDouble(value);
        public byte   Convert(double value) => SafeConvert.ToByte(value);
    }

    public class DoubleToIntConverter : IPropertyConverter<double, int>
    {
        public double Convert(int value)    => SafeConvert.ToDouble(value);
        public int    Convert(double value) => SafeConvert.ToInt32(value);
    }

    public class DoubleToSingleConverter : IPropertyConverter<double, float>
    {
        public double Convert(float value)  => SafeConvert.ToDouble(value);
        public float  Convert(double value) => SafeConvert.ToSingle(value);
    }

    public class DoubleToStringConverter : IPropertyConverter<double, string>
    {
        public double Convert(string value) => SafeConvert.ToDouble(value);
        public string Convert(double value) => SafeConvert.ToString(value);
    }
}