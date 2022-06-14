using FluentAssertions;
using SchauerMarkenerkennung.MVVM.View;
using SchauerMarkenerkennung.MVVM.ViewModel;

namespace MarkenerkennungXUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //ScanView scanView = new ScanView();
            string expected = "123456789";
            string actual = "123456789";
            actual.Should().Be(expected);
        }

        [Fact]
        public void Test2()
        {
            ExportView scanView = new ExportView();
            var actual = scanView.getString();
            List<string> expected = new List<string>();
            //expected.Add();
            //actual.Should().Be(expected);
        }
    }
}