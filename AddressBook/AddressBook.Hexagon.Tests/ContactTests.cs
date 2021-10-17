//By Bart Vertongen copyright 2021.

using Xunit;
using PS.AddressBook.Hexagon.Domain.Ports;


namespace PS.AddressBook.Hexagon.Domain.Tests
{
    public class ContactTests
    {
        [Fact]
        public void Construction_NoData_ShouldGiveInvalidContact()
        {
            //Arrange
            IContact aContact;

            //Actions
            aContact = new Contact();

            //Asserts
            Assert.False(aContact.IsValid());
        }

        [Fact]
        public void Construction_AllValidData_ShouldGiveValidContact()
        {
            //Arrange
            IAddress anAddress;
            IContact aContact;

            //Actions
            anAddress = new Address("Weverijstraat 12", "9500", "Geraardsbergen");
            aContact = new Contact
            {
                Name = "Oscar Degrave",
                Address = anAddress,
                Phone = "054/45.14.78",
                Email = "odegrave@telenet.be"
            };

            //Asserts
            Assert.True(aContact.IsValid());
        }


        [Fact]
        public void Construction_WithNameAndAddress_ShouldGiveInvalidContact()
        {
            //Arrange
            IAddress anAddress;
            IContact aContact;

            //Actions
            anAddress = new Address("Weverijstraat 12", "9500", "Geraardsbergen");
            aContact = new Contact
            {
                Name = "Oscar Degrave",
                Address = anAddress
            };

            //Asserts
            Assert.False(aContact.IsValid());
        }

        [Fact]
        public void Construction_WithNameAndPhone_ShouldGiveValidContact()
        {
            //Arrange
            IContact aContact;

            //Actions
            aContact = new Contact
            {
                Name = "Oscar Degrave",
                Phone = "054/48.74.64"
            };

            //Asserts
            Assert.True(aContact.IsValid());
        }

        [Fact]
        public void Construction_WithNameAndEmail_ShouldGiveValidContact()
        {
            //Arrange
            IContact aContact;

            //Actions
            aContact = new Contact
            {
                Name = "Oscar Degrave",
                Email = "ograve@telenet.be"
            };

            //Asserts
            Assert.True(aContact.IsValid());
        }

        [Fact]
        public void Construction_WithNameOnly_ShouldGiveEmptyContentsCode()
        {
            //Arrange
            IContact aContact;

            //Actions
            aContact = new Contact
            {
                Name = "Oscar Degrave"
            };

            //Asserts
            Assert.True(aContact.ContentsCode == "***");
        }

        [Fact]
        public void Construction_WithNameAndAddress_ShouldGiveContentsCodeA()
        {
            //Arrange
            IContact aContact;
            IAddress anAddress;

            //Actions
            aContact = new Contact
            {
                Name = "Oscar Degrave"
            };
            anAddress = new Address("Weverijstraat 12", "9500", "Geraardsbergen");
            aContact.Address = anAddress;

            //Asserts
            Assert.True(aContact.ContentsCode == "A**");
        }

        [Fact]
        public void Construction_WithNameAndPhone_ShouldGiveContentsCodeP()
        {
            //Arrange
            IContact aContact;

            //Actions
            aContact = new Contact
            {
                Name = "Oscar Degrave",
                Phone = "054/48.74.64"
            };

            //Asserts
            Assert.True(aContact.ContentsCode == "*P*");
        }
    }
}