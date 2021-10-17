// Bart Vertongen copyright 2021.

using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using PS.AddressBook.Hexagon.Domain.Ports;
using PS.AddressBook.Hexagon.Application.Ports.Out;


namespace PS.AddressBook.Hexagon.Domain.Tests
{
    public class AddressBookTests
    {
        private readonly IAddressBookFile _DALAddressBook;

        public AddressBookTests()
        {
            string sFullPath = Environment.CurrentDirectory + "\\AddressBook.xml";

            Mock<IConfigurationRoot> MockConfig = new();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(sFullPath);

            Mock<IAddressBookFile> MockDSAddressBook = new();
            _DALAddressBook = MockDSAddressBook.Object;
        }

        [Fact]
        public void Construction_NoData_ShouldGiveEmptyAddressBook()
        {
            //Arrange
            IAddressBook anAddressBook;

            //Actions
            anAddressBook = new AddressBook();

            //Asserts
            Assert.NotNull(anAddressBook);
        }

        [Fact]
        public void NewAddressBook_AddValidContact_ShouldSucceed()
        {
            //Arrange
            IAddressBook anAddressBook;

            //Actions
            anAddressBook = new AddressBook();
            IContact ValidContact = new Contact
            {
                Name = "Elizabeth De Prinses",
                Phone = "02/581.14.78"
            };
            anAddressBook.Add(ValidContact);

            //Asserts
            Assert.NotNull(anAddressBook);
            Assert.True(ValidContact.IsValid());
        }

        [Fact]
        public void NewAddressBook_AddInvalidContact_ShouldFail()
        {
            //Arrange
            IAddressBook anAddressBook;
            Contact InvalidContact;

            //Actions
            anAddressBook = new AddressBook();
            InvalidContact = new Contact
            {
                Name = "Elizabeth De Prinses"
            };
            void testCode() => anAddressBook.Add(InvalidContact); //a local function
            var ex = Record.Exception(testCode);

            //Asserts
            Assert.NotNull(anAddressBook);
            Assert.False(InvalidContact.IsValid());
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
        }

        [Fact]
        public void NewAddressBook_AddExistingContactSameReference_ShouldFail()
        {
            //Arrange
            IAddressBook anAddressBook;
            IContact ValidContact;

            //Actions
            anAddressBook = new AddressBook();
            ValidContact = new Contact
            {
                Name = "Elizabeth De Prinses",
                Phone = "02/581.14.78"
            };
            anAddressBook.Add(ValidContact);
            Action testCode = () => anAddressBook.Add(ValidContact);
            var ex = Record.Exception(testCode);

            //Asserts
            Assert.NotNull(anAddressBook);
            Assert.True(ValidContact.IsValid());
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
        }

        [Fact]
        public void NewAddressBook_AddExistingContactOtherReference_ShouldFail()
        {
            //Arrange
            IAddressBook anAddressBook;
            Contact ValidContact, IdemContact;

            //Actions
            anAddressBook = new AddressBook();
            ValidContact = new Contact
            {
                Name = "Elizabeth De Prinses",
                Phone = "02/581.14.78"
            };
            IdemContact = (Contact)ValidContact.DeepClone();
            anAddressBook.Add(ValidContact);
            Action testCode = () => anAddressBook.Add(IdemContact);
            var ex = Record.Exception(testCode);

            //Asserts
            Assert.NotNull(anAddressBook);
            Assert.True(ValidContact.IsValid());
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
        }
    }
}