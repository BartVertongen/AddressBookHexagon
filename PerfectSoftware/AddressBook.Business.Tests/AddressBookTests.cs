// Bart Vertongen copyright 2021.

using System;
using System.IO;
using Xunit;


namespace PS.AddressBook.Business.Tests
{
    public class AddressBookTests
    {
        [Fact]
        public void Construction_NoData_ShouldGiveEmptyAddressBook()
        {
            //Arrange
            AddressBook anAddressBook;

            //Actions
            anAddressBook = new AddressBook();

            //Asserts
            Assert.NotNull(anAddressBook);
            Assert.True(anAddressBook.Count == 0);
        }

        [Fact]
        public void NewAddressBook_AddValidContact_ShouldSucceed()
        {
            //Arrange
            AddressBook anAddressBook;
            Contact ValidContact;

            //Actions
            anAddressBook = new AddressBook();
            ValidContact = new Contact();
            ValidContact.Name = "Elizabeth De Prinses";
            ValidContact.PhoneNumber = "02/581.14.78";
            anAddressBook.Add(ValidContact);

            //Asserts
            Assert.NotNull(anAddressBook);
            Assert.True(ValidContact.IsValid());
            Assert.True(anAddressBook.Count == 1);
        }

        [Fact]
        public void NewAddressBook_AddInvalidContact_ShouldFail()
        {
            //Arrange
            AddressBook anAddressBook;
            Contact InvalidContact;

            //Actions
            anAddressBook = new AddressBook();
            InvalidContact = new Contact();
            InvalidContact.Name = "Elizabeth De Prinses";
            void testCode() => anAddressBook.Add(InvalidContact); //a local function
            var ex = Record.Exception(testCode);

            //Asserts
            Assert.NotNull(anAddressBook);
            Assert.False(InvalidContact.IsValid());
            Assert.True(anAddressBook.Count == 0);
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
        }

        [Fact]
        public void NewAddressBook_AddExistingContactSameReference_ShouldFail()
        {
            //Arrange
            AddressBook anAddressBook;
            Contact ValidContact;

            //Actions
            anAddressBook = new AddressBook();
            ValidContact = new Contact();
            ValidContact.Name = "Elizabeth De Prinses";
            ValidContact.PhoneNumber = "02/581.14.78";
            anAddressBook.Add(ValidContact);
            Action testCode = () => anAddressBook.Add(ValidContact);
            var ex = Record.Exception(testCode);

            //Asserts
            Assert.NotNull(anAddressBook);
            Assert.True(ValidContact.IsValid());
            Assert.True(anAddressBook.Count == 1);
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
        }

        [Fact]
        public void NewAddressBook_AddExistingContactOtherReference_ShouldFail()
        {
            //Arrange
            AddressBook anAddressBook;
            Contact ValidContact, IdemContact;

            //Actions
            anAddressBook = new AddressBook();
            ValidContact = new Contact();
            ValidContact.Name = "Elizabeth De Prinses";
            ValidContact.PhoneNumber = "02/581.14.78";
            IdemContact = ValidContact.DeepClone();
            anAddressBook.Add(ValidContact);
            Action testCode = () => anAddressBook.Add(IdemContact);
            var ex = Record.Exception(testCode);

            //Asserts
            Assert.NotNull(anAddressBook);
            Assert.True(ValidContact.IsValid());
            Assert.True(anAddressBook.Count == 1);
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
        }
    }
}