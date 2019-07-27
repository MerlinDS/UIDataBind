using System;
using NSubstitute;
using NUnit.Framework;
using UIDataBindCore.Core;

namespace UIDataBindCoreTests
{
    [TestFixture]
    public class BindingKernelTestFixture
    {
        [Test]
        public void TempTest()
        {
            var context = Substitute.For<IDataContext>();
            Console.WriteLine(context.ToString());
            BindingKernel.Instance.Register();
        }
    }
}