// Bart Vertongen copyright 2021.

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using PS.AddressBook.Data;
using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Business.Tests
{
    public class AddressBookTests
    {
        class DSAddressBookMock : IDSAddressBook
        {
            public DSAddressBookMock(IConfigurationRoot config)
            {
                FullPath = config.GetSection("ContactsFile").Value;
            }

            public string FullPath { get; private set; }

            public void Load(IList<IContactDTO> book)
            {
            }

            public void Save(IList<IContactDTO> book)
            {
                throw new NotImplementedException();
            }
        }

        private readonly IConfigurationRoot _Configuration;
        private readonly IDSAddressBook _DSAddressBook;

        public AddressBookTests()
        {
            string sFullPath = Environment.CurrentDirectory + "\\AddressBook.xml";

            Mock<IConfigurationRoot> MockConfig = new Mock<IConfigurationRoot>();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(sFullPath);

            _DSAddressBook = new DSAddressBook(MockConfig.Object);
        }

        [Fact]
        public void Construction_NoData_ShouldGiveEmptyAddressBook()
        {
            //Arrange
            AddressBook anAddressBook;

            //Actions
            anAddressBook = new AddressBook(_DSAddressBook);

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
            anAddressBook = new AddressBook(_DSAddressBook);
            ValidContact = new Contact
            {
                Name = "Elizabeth De Prinses",
                PhoneNumber = "02/581.14.78"
            };
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
            anAddressBook = new AddressBook(_DSAddressBook);
            InvalidContact = new Contact
            {
                Name = "Elizabeth De Prinses"
            };
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
            anAddressBook = new AddressBook(_DSAddressBook);
            ValidContact = new Contact
            {
                Name = "Elizabeth De Prinses",
                PhoneNumber = "02/581.14.78"
            };
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
            anAddressBook = new AddressBook(_DSAddressBook);
            ValidContact = new Contact
            {
                Name = "Elizabeth De Prinses",
                PhoneNumber = "02/581.14.78"
            };
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