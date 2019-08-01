using System;
using UIDataBindCore.Converters;

namespace UIDataBindCore.Extensions
{
    public static class BuildInConvertersExtension
    {
        public static ConvertersCollection RegisterBuildIn(this ConvertersCollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            collection.RegisterBoolean();
            collection.RegisterByte();
            collection.RegisterInt32();
            collection.RegisterSingle();
            collection.RegisterDouble();
            collection.RegisterString();
            return collection;
        }

        private static void RegisterBoolean(this ConvertersCollection collection)
        {
            collection.Register(new BooleanToByteConverter());
            collection.Register(new BooleanToIntConverter());
            collection.Register(new BooleanToSingleConverter());
            collection.Register(new BooleanToDoubleConverter());
            collection.Register(new BooleanToStringConverter());
        }

        private static void RegisterByte(this ConvertersCollection collection)
        {
            collection.Register(new ByteToBooleanConverter());
            collection.Register(new ByteToIntConverter());
            collection.Register(new ByteToSingleConverter());
            collection.Register(new ByteToDoubleConverter());
            collection.Register(new ByteToStringConverter());
        }

        private static void RegisterInt32(this ConvertersCollection collection)
        {
            collection.Register(new IntToBooleanConverter());
            collection.Register(new IntToByteConverter());
            collection.Register(new IntToSingleConverter());
            collection.Register(new IntToDoubleConverter());
            collection.Register(new IntToStringConverter());
        }

        private static void RegisterSingle(this ConvertersCollection collection)
        {
            collection.Register(new SingleToBooleanConverter());
            collection.Register(new SingleToByteConverter());
            collection.Register(new SingleToIntConverter());
            collection.Register(new SingleToDoubleConverter());
            collection.Register(new SingleToStringConverter());
        }

        private static void RegisterDouble(this ConvertersCollection collection)
        {
            collection.Register(new DoubleToBooleanConverter());
            collection.Register(new DoubleToByteConverter());
            collection.Register(new DoubleToIntConverter());
            collection.Register(new DoubleToSingleConverter());
            collection.Register(new DoubleToStringConverter());
        }

        private static void RegisterString(this ConvertersCollection collection)
        {
            collection.Register(new StringToBooleanConverter());
            collection.Register(new StringToByteConverter());
            collection.Register(new StringToIntConverter());
            collection.Register(new StringToSingleConverter());
            collection.Register(new StringToDoubleConverter());
        }
    }
}