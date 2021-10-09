// Bart Vertongen copyright 2021.

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using PS.AddressBook.Hexagon.Domain.Core;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.Hexagon.Application.Tests
{
    public class AddressBookServiceTests
    {
        class AddressBookFileMock : IAddressBookFile
        {
            public AddressBookFileMock(IConfigurationRoot config)
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

        private readonly IAddressBookFile   _DSAddressBook;
        private readonly IAddressBook       _AddressBook;

        public AddressBookServiceTests()
        {
            string sFullPath = Environment.CurrentDirectory + "\\AddressBook.xml";

            Mock<IConfigurationRoot> MockConfig = new();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(sFullPath);

            _DSAddressBook = new AddressBookFileMock(MockConfig.Object);
            //TODO Can we Mock this ?
            _AddressBook = new BussAddressBook(_DSAddressBook);
        }

        [Fact]
        public void Construction_NoData_ShouldGiveValidAddressBookService()
        {
            //Arrange
            IAddressBookService ABService;

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
            Mock<IAddressDTO> MockAddress = new();
            MockAddress.SetupGet(a => a.Street).Returns("");
            MockAddress.SetupGet(a => a.PostalCode).Returns("");
            MockAddress.SetupGet(a => a.Town).Returns("");
            IContactDTO ValidContact;
            Mock<IContactDTO> MockContact = new();
            MockContact.SetupGet(p => p.Name).Returns("Elizabeth De Prinses");
            MockContact.SetupGet(p => p.PhoneNumber).Returns("02/581.14.78");
            MockContact.SetupGet(p => p.Email).Returns("");
            MockContact.SetupGet(p => p.Address).Returns(MockAddress.Object);

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
            Mock<IAddressDTO> MockAddress = new();
            MockAddress.SetupGet(a => a.Street).Returns("");
            MockAddress.SetupGet(a => a.PostalCode).Returns("");
            MockAddress.SetupGet(a => a.Town).Returns("");
            IContactDTO InvalidContact;
            Mock<IContactDTO> MockContact = new();
            MockContact.SetupGet(p => p.Name).Returns("Elizabeth De Prinses");
            MockContact.SetupGet(p => p.PhoneNumber).Returns("");
            MockContact.SetupGet(p => p.Email).Returns("");
            MockContact.SetupGet(p => p.Address).Returns(MockAddress.Object);

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
            Mock<IAddressDTO> MockAddress = new();
            MockAddress.SetupGet(a => a.Street).Returns("");
            MockAddress.SetupGet(a => a.PostalCode).Returns("");
            MockAddress.SetupGet(a => a.Town).Returns("");
            Mock<IContactDTO> MockContact = new();
            MockContact.SetupGet(p => p.Name).Returns("Elizabeth De Prinses");
            MockContact.SetupGet(p => p.PhoneNumber).Returns("02/581.14.78");
            MockContact.SetupGet(p => p.Email).Returns("");
            MockContact.SetupGet(p => p.Address).Returns(MockAddress.Object);

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