using UIDataBindCore.Utils;

namespace UIDataBindCore.Converters
{
    public class BooleanToByteConverter : IPropertyConverter<bool, byte>
    {
        public bool Convert(byte value) => SafeConvert.ToBoolean(value);
        public byte Convert(bool value) => SafeConvert.ToByte(value);
    }

    public class BooleanToIntConverter : IPropertyConverter<bool, int>
    {
        public bool Convert(int value)  => SafeConvert.ToBoolean(value);
        public int  Convert(bool value) => SafeConvert.ToInt32(value);
    }

    public class BooleanToSingleConverter : IPropertyConverter<bool, float>
    {
        public bool  Convert(float value) => SafeConvert.ToBoolean(value);
        public float Convert(bool value)  => SafeConvert.ToSingle(value);
    }

    public class BooleanToDoubleConverter : IPropertyConverter<bool, double>
    {
        public bool   Convert(double value) => SafeConvert.ToBoolean(value);
        public double Convert(bool value)   => SafeConvert.ToDouble(value);
    }

    public class BooleanToStringConverter : IPropertyConverter<bool, string>
    {
        public bool   Convert(string value) => SafeConvert.ToBoolean(value);
        public string Convert(bool value)   => SafeConvert.ToString(value);
    }
}