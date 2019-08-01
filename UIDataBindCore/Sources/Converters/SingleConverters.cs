using UIDataBindCore.Utils;

namespace UIDataBindCore.Converters
{
    public class SingleToBooleanConverter : IPropertyConverter<float, bool>
    {
        public float Convert(bool value)  => SafeConvert.ToSingle(value);
        public bool  Convert(float value) => SafeConvert.ToBoolean(value);
    }

    public class SingleToByteConverter : IPropertyConverter<float, byte>
    {
        public float Convert(byte value)  => SafeConvert.ToSingle(value);
        public byte  Convert(float value) => SafeConvert.ToByte(value);
    }

    public class SingleToIntConverter : IPropertyConverter<float, int>
    {
        public float Convert(int value)   => SafeConvert.ToSingle(value);
        public int   Convert(float value) => SafeConvert.ToInt32(value);
    }

    public class SingleToDoubleConverter : IPropertyConverter<float, double>
    {
        public float  Convert(double value) => SafeConvert.ToSingle(value);
        public double Convert(float value)  => SafeConvert.ToDouble(value);
    }

    public class SingleToStringConverter : IPropertyConverter<float, string>
    {
        public float  Convert(string value) => SafeConvert.ToSingle(value);
        public string Convert(float value)  => SafeConvert.ToString(value);
    }
}