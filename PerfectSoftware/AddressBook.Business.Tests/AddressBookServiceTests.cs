// Bart Vertongen copyright 2021.

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.Business.Tests
{
    public class AddressBookServiceTests
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
            }
        }

        //private readonly IConfigurationRoot _Configuration;
        private readonly IDSAddressBook _DSAddressBook;
        private readonly AddressBook    _AddressBook;

        public AddressBookServiceTests()
        {
            string sFullPath = Environment.CurrentDirectory + "\\AddressBook.xml";

            Mock<IConfigurationRoot> MockConfig = new();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(sFullPath);

            _DSAddressBook = new DSAddressBookMock(MockConfig.Object);
            _AddressBook = new AddressBook(_DSAddressBook);
        }

        [Fact]
        public void Construction_NoData_ShouldGiveValidAddressBookService()
        {
            //Arrange
            AddressBookService ABService;

            //Actions
            ABService = new AddressBookService(_AddressBook);

            //Asserts
            Assert.NotNull(ABService);
        }

        [Fact]
        public void NewAddressBookService_AddValidContactDTO_ShouldSucceed()
        {
            //Arrange
            AddressBookService ABService;
            IContactDTO ValidContact;
            Mock<IContactDTO> MockContact = new();
            MockContact.SetupGet(p => p.Name).Returns("Elizabeth De Prinses");
            MockContact.SetupGet(p => p.PhoneNumber).Returns("02/581.14.78");

            //Actions
            ABService = new AddressBookService(_AddressBook);
            ValidContact = MockContact.Object;
            ABService.Add(ValidContact);

            //Asserts
            Assert.True(_AddressBook.Count == 1);
        }

        [Fact]
        public void NewAddressBookService_AddInvalidContactDTO_ShouldFail()
        {
            //Arrange
            AddressBookService ABService;
            IContactDTO InvalidContact;
            Mock<IContactDTO> MockContact = new();
            MockContact.SetupGet(p => p.Name).Returns("Elizabeth De Prinses");

            //Actions
            ABService = new AddressBookService(_AddressBook);
            InvalidContact = MockContact.Object;
            void testCode() => ABService.Add(InvalidContact); //a local function
            var ex = Record.Exception(testCode);

            //Asserts
            Assert.True(_AddressBook.Count == 0);
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
        }

        [Fact]
        public void NewAddressBookService_AddExistingContactDTO_ShouldFail()
        {
            //Arrange
            AddressBookService ABService;
            IContactDTO ValidContact;
            Mock<IContactDTO> MockContact = new();
            MockContact.SetupGet(p => p.Name).Returns("Elizabeth De Prinses");
            MockContact.SetupGet(p => p.PhoneNumber).Returns("02/581.14.78");

            //Actions
            ABService = new AddressBookService(_AddressBook);
            ValidContact = MockContact.Object;
            ABService.Add(ValidContact);
            Action testCode = () => ABService.Add(ValidContact);
            var ex = Record.Exception(testCode);

            //Asserts
            Assert.True(_AddressBook.Count == 1);
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
        }
    }
}