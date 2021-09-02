//Copyright 2021 Bart Vertongen.

using Xunit;


namespace PS.AddressBook.Business.Tests
{
    public class AddressTests
    {
        [Fact]
        public void Construction_NoData_ShouldGiveEmptyAddress()
        {
            //Arrange
            Address anAddress;

            //Actions
            anAddress = new Address();

            //Asserts
            Assert.NotNull(anAddress);
            Assert.Equal("", anAddress.Street);
            Assert.Equal("", anAddress.PostalCode);
            Assert.Equal("", anAddress.Town);
        }

        [Fact]
        public void Construction_AllValidData_ShouldGiveValidAddress()
        {
            //Arrange
            Address anAddress;

            //Actions
            anAddress = new Address("Weverijstraat 12", "9500", "Geraardsbergen");

            //Asserts
            Assert.NotNull(anAddress);
            Assert.Equal("Weverijstraat 12", anAddress.Street);
            Assert.Equal("9500", anAddress.PostalCode);
            Assert.Equal("Geraardsbergen", anAddress.Town);
        }

        [Fact]
        public void Construction_WithNoStreet_ShouldGiveEmptyAddress()
        {
            //Arrange
            Address anAddress;

            //Actions
            anAddress = new Address("", "9500", "Geraardsbergen");

            //Asserts
            Assert.NotNull(anAddress);
            Assert.Equal("", anAddress.Street);
            Assert.Equal("", anAddress.PostalCode);
            Assert.Equal("", anAddress.Town);
        }

        [Fact]
        public void Construction_WithNoPostalCode_ShouldGiveEmptyAddress()
        {
            //Arrange
            Address anAddress;

            //Actions
            anAddress = new Address("Weverijstraat 12", "", "Geraardsbergen");

            //Asserts
            Assert.NotNull(anAddress);
            Assert.Equal("", anAddress.Street);
            Assert.Equal("", anAddress.PostalCode);
            Assert.Equal("", anAddress.Town);
        }

        [Fact]
        public void Construction_WithNoTown_ShouldGiveEmptyAddress()
        {
            //Arrange
            Address anAddress;

            //Actions
            anAddress = new Address("Weverijstraat 12", "9500", "");

            //Asserts
            Assert.NotNull(anAddress);
            Assert.Equal("", anAddress.Street);
            Assert.Equal("", anAddress.PostalCode);
            Assert.Equal("", anAddress.Town);
        }
    }
}
