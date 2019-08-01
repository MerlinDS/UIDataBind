using UIDataBindCore.Utils;

namespace UIDataBindCore.Converters
{
    public class StringToBooleanConverter : IPropertyConverter<string, bool>
    {
        public string Convert(bool value) => SafeConvert.ToString(value);
        public bool Convert(string value) => SafeConvert.ToBoolean(value);
    }
    
    public class StringToByteConverter : IPropertyConverter<string, byte>
    {
        public string Convert(byte value) => SafeConvert.ToString(value);
        public byte Convert(string value) => SafeConvert.ToByte(value);
    }

    public class StringToIntConverter : IPropertyConverter<string, int>
    {
        public string Convert(int value)  => SafeConvert.ToString(value);
        public int  Convert(string value) => SafeConvert.ToInt32(value);
    }

    public class StringToSingleConverter : IPropertyConverter<string, float>
    {
        public string  Convert(float value) => SafeConvert.ToString(value);
        public float Convert(string value)  => SafeConvert.ToSingle(value);
    }

    public class StringToDoubleConverter : IPropertyConverter<string, double>
    {
        public string   Convert(double value) => SafeConvert.ToString(value);
        public double Convert(string value)   => SafeConvert.ToDouble(value);
    }
}