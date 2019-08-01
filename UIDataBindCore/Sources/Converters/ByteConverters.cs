using UIDataBindCore.Utils;

namespace UIDataBindCore.Converters
{
    public class ByteToBooleanConverter : IPropertyConverter<byte, bool>
    {
        public byte Convert(bool value) => SafeConvert.ToByte(value);
        public bool Convert(byte value) => SafeConvert.ToBoolean(value);
    }

    public class ByteToIntConverter : IPropertyConverter<byte, int>
    {
        public byte Convert(int value)  => SafeConvert.ToByte(value);
        public int  Convert(byte value) => SafeConvert.ToInt32(value);
    }

    public class ByteToSingleConverter : IPropertyConverter<byte, float>
    {
        public byte  Convert(float value) => SafeConvert.ToByte(value);
        public float Convert(byte value)  => SafeConvert.ToSingle(value);
    }

    public class ByteToDoubleConverter : IPropertyConverter<byte, double>
    {
        public byte   Convert(double value) => SafeConvert.ToByte(value);
        public double Convert(byte value)   => SafeConvert.ToDouble(value);
    }

    public class ByteToStringConverter : IPropertyConverter<byte, string>
    {
        public byte   Convert(string value) => SafeConvert.ToByte(value);
        public string Convert(byte value)   => SafeConvert.ToString(value);
    }
}