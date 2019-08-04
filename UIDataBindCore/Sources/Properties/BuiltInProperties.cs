namespace UIDataBindCore.Properties
{
    /**
     * Syntax sugar that provides classes for fast BindProperty creation
     */
    public sealed class BooleanProperty : BindProperty<bool>
    {
        public BooleanProperty(bool value = default) : base(value)
        {
        }
    }

    public sealed class ByteProperty : BindProperty<byte>
    {
        public ByteProperty(byte value = default) : base(value)
        {
        }
    }

    public sealed class IntProperty : BindProperty<int>
    {
        public IntProperty(int value = default) : base(value)
        {
        }
    }

    public sealed class FloatProperty : BindProperty<float>
    {
        public FloatProperty(float value = default) : base(value)
        {
        }
    }

    public sealed class DoubleProperty : BindProperty<double>
    {
        public DoubleProperty(double value = default) : base(value)
        {
        }
    }

    public sealed class StringProperty : BindProperty<string>
    {
        public StringProperty(string value = default) : base(value)
        {
        }
    }
}