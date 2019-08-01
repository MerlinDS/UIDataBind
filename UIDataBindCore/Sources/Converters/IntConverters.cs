using UIDataBindCore.Utils;

namespace UIDataBindCore.Converters
{
    public class IntToBooleanConverter : IPropertyConverter<int, bool>
    {
        public int  Convert(bool value) => SafeConvert.ToInt32(value);
        public bool Convert(int value)  => SafeConvert.ToBoolean(value);
    }

    public class IntToByteConverter : IPropertyConverter<int, byte>
    {
        public int  Convert(byte value) => SafeConvert.ToInt32(value);
        public byte Convert(int value)  => SafeConvert.ToByte(value);
    }

    public class IntToSingleConverter : IPropertyConverter<int, float>
    {
        public int   Convert(float value) => SafeConvert.ToInt32(value);
        public float Convert(int value)   => SafeConvert.ToSingle(value);
    }

    public class IntToDoubleConverter : IPropertyConverter<int, double>
    {
        public int    Convert(double value) => SafeConvert.ToInt32(value);
        public double Convert(int value)    => SafeConvert.ToDouble(value);
    }

    public class IntToStringConverter : IPropertyConverter<int, string>
    {
        public int    Convert(string value) => SafeConvert.ToInt32(value);
        public string Convert(int value)    => SafeConvert.ToString(value);
    }
}