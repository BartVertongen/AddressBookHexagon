//By Bart Vertongen copyright 2021.

using Xunit;


namespace PS.AddressBook.Business.Tests
{
    public class ContactTests
    {
        [Fact]
        public void Construction_NoData_ShouldGiveInvalidContact()
        {
            //Arrange
            Contact aContact;

            //Actions
            aContact = new Contact();

            //Asserts
            Assert.False(aContact.IsValid());
        }

        [Fact]
        public void Construction_AllValidData_ShouldGiveValidContact()
        {
            //Arrange
            Address anAddress;
            Contact aContact;

            //Actions
            anAddress = new Address("Weverijstraat 12", "9500", "Geraardsbergen");
            aContact = new Contact
            {
                Name = "Oscar Degrave",
                Address = anAddress,
                PhoneNumber = "054/45.14.78",
                Email = "odegrave@telenet.be"
            };

            //Asserts
            Assert.True(aContact.IsValid());
        }


        [Fact]
        public void Construction_WithNameAndAddress_ShouldGiveInvalidContact()
        {
            //Arrange
            Address anAddress;
            Contact aContact;

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
            Contact aContact;

            //Actions
            aContact = new Contact
            {
                Name = "Oscar Degrave",
                PhoneNumber = "054/48.74.64"
            };

            //Asserts
            Assert.True(aContact.IsValid());
        }

        [Fact]
        public void Construction_WithNameAndEmail_ShouldGiveValidContact()
        {
            //Arrange
            Contact aContact;

            //Actions
            aContact = new Contact();
            aContact.Name = "Oscar Degrave";
            aContact.Email = "ograve@telenet.be";

            //Asserts
            Assert.True(aContact.IsValid());
        }

        [Fact]
        public void Construction_WithNameOnly_ShouldGiveEmptyContentsCode()
        {
            //Arrange
            Contact aContact;

            //Actions
            aContact = new Contact();
            aContact.Name = "Oscar Degrave";

            //Asserts
            Assert.True(aContact.ContentsCode == "***");
        }

        [Fact]
        public void Construction_WithNameAndAddress_ShouldGiveContentsCodeA()
        {
            //Arrange
            Contact aContact;
            Address anAddress;

            //Actions
            aContact = new Contact();
            aContact.Name = "Oscar Degrave";
            anAddress = new Address("Weverijstraat 12", "9500", "Geraardsbergen");
            aContact.Address = anAddress;

            //Asserts
            Assert.True(aContact.ContentsCode == "A**");
        }

        [Fact]
        public void Construction_WithNameAndPhone_ShouldGiveContentsCodeP()
        {
            //Arrange
            Contact aContact;

            //Actions
            aContact = new Contact();
            aContact.Name = "Oscar Degrave";
            aContact.PhoneNumber = "054/48.74.64";

            //Asserts
            Assert.True(aContact.ContentsCode == "*P*");
        }
    }
}