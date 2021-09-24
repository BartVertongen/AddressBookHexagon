
using Xunit;


namespace PS.AddressBook.UI.Tests
{
    public class InputIteratorTest
    {
        [Fact]
        public void Constructor_NoData_ShouldGiveNoInput()
        {
            //Arrange
            string sFirstInput, sSecondInput;
            IInputIterator Inputs = new InputIterator(null, "-1", null, null, null, null, null, null);

            //Action
            sFirstInput = Inputs.GetInput();
            sSecondInput = Inputs.GetInput();

            //Assert
            Assert.Null(sFirstInput);
            Assert.Null(sSecondInput);
        }

        [Fact]
        public void Constructor_OnlyFilter_ShouldGiveOnlyFilter()
        {
            //Arrange
            string sFirstInput, sSecondInput;
            IInputIterator Inputs = new InputIterator("abba", "-1", null, null, null, null, null, null);

            //Action
            sFirstInput = Inputs.GetInput();
            sSecondInput = Inputs.GetInput();

            //Assert
            Assert.Equal("abba", sFirstInput);
            Assert.Null(sSecondInput);
        }

        [Fact]
        public void Constructor_OnlyEmail_ShouldGiveOnlyEmail()
        {
            //Arrange
            string sFirstInput;
            IInputIterator Inputs = new InputIterator(null, "-1", null, null, null, null, null, "anyone@server.com");

            //Action
            sFirstInput = Inputs.GetInput();

            //Assert
            Assert.Equal("anyone@server.com", sFirstInput);
        }

        [Fact]
        public void Constructor_AllData_ShouldGiveAllData()
        {
            //Arrange
            string sFirstInput, sSecondInput, sThirdInput, sFourthInput;
            string sFifthInput, sSixthInput, sSeventhInput, sEighthInput;
            IInputIterator Inputs = new InputIterator("a", "1", "Joe Dalton", "Cactusstreet 5", "9900", "Desert", "1711", "gun@gmail.com");

            //Action
            sFirstInput = Inputs.GetInput();
            sSecondInput = Inputs.GetInput();
            sThirdInput = Inputs.GetInput();
            sFourthInput = Inputs.GetInput();
            sFifthInput = Inputs.GetInput();
            sSixthInput = Inputs.GetInput();
            sSeventhInput = Inputs.GetInput();
            sEighthInput = Inputs.GetInput();

            //Assert
            Assert.Equal("a", sFirstInput);
            Assert.Equal("1", sSecondInput);
            Assert.Equal("Joe Dalton", sThirdInput);
            Assert.Equal("Cactusstreet 5", sFourthInput);
            Assert.Equal("9900", sFifthInput);
            Assert.Equal("Desert", sSixthInput);
            Assert.Equal("1711", sSeventhInput);
            Assert.Equal("gun@gmail.com", sEighthInput);
        }

        [Fact]
        public void Constructor_AlternatingData_ShouldGiveAlternatingData()
        {
            //Arrange
            string sFirstInput, sSecondInput, sThirdInput, sFourthInput;
            string sFifthInput, sSixthInput, sSeventhInput, sEighthInput;
            IInputIterator Inputs = new InputIterator("a", "-1", "Joe Dalton", null, "9900", null, "1711", null);

            //Action
            sFirstInput = Inputs.GetInput();
            sSecondInput = Inputs.GetInput();
            sThirdInput = Inputs.GetInput();
            sFourthInput = Inputs.GetInput();
            sFifthInput = Inputs.GetInput();
            sSixthInput = Inputs.GetInput();
            sSeventhInput = Inputs.GetInput();
            sEighthInput = Inputs.GetInput();

            //Assert
            Assert.Equal("a", sFirstInput);
            Assert.Equal("Joe Dalton", sSecondInput);
            Assert.Equal("9900", sThirdInput);
            Assert.Equal("1711", sFourthInput);
            Assert.Null(sFifthInput);
            Assert.Null(sSixthInput);
            Assert.Null(sSeventhInput);
            Assert.Null(sEighthInput);
        }

        [Fact]
        public void Constructor_ReadTooMuch_ShouldGiveNull()
        {
            //Arrange
            string sFirstInput, sSecondInput, sThirdInput, sFourthInput;
            string sFifthInput, sSixthInput, sSeventhInput, sEighthInput, sNinethInput;
            IInputIterator Inputs = new InputIterator("a", "1", "Joe Dalton", "Cactusstreet 5", "9900", "Desert", "1711", "gun@gmail.com");

            //Action
            sFirstInput = Inputs.GetInput();
            sSecondInput = Inputs.GetInput();
            sThirdInput = Inputs.GetInput();
            sFourthInput = Inputs.GetInput();
            sFifthInput = Inputs.GetInput();
            sSixthInput = Inputs.GetInput();
            sSeventhInput = Inputs.GetInput();
            sEighthInput = Inputs.GetInput();
            sNinethInput = Inputs.GetInput();

            //Assert
            Assert.Equal("a", sFirstInput);
            Assert.Equal("1", sSecondInput);
            Assert.Equal("Joe Dalton", sThirdInput);
            Assert.Equal("Cactusstreet 5", sFourthInput);
            Assert.Equal("9900", sFifthInput);
            Assert.Equal("Desert", sSixthInput);
            Assert.Equal("1711", sSeventhInput);
            Assert.Equal("gun@gmail.com", sEighthInput);
            Assert.Null(sNinethInput);
        }
    }
}
