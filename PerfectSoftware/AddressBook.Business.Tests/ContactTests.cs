using System;
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
            aContact = new Contact();
            aContact.Name = "Oscar Degrave";
            aContact.Address = anAddress;
            aContact.PhoneNumber = "054/45.14.78";
            aContact.Email = "odegrave@telenet.be";

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
            aContact = new Contact();
            aContact.Name = "Oscar Degrave";
            aContact.Address = anAddress;

            //Asserts
            Assert.False(aContact.IsValid());
        }
    }
}
