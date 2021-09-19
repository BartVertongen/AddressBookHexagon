//Copyright 2021 Bart Vertongen.

using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using PS.AddressBook.Business;
using PS.AddressBook.UI;
using PS.AddressBook.UI.Commands;
using PS.AddressBook.Business.Interfaces;


namespace UseCaseTests2
{
    /// <summary>
    /// Give Overview of All Contacts with possible filtering
    /// </summary>
    public class UseCase1Test2
    {
        private readonly AddressBook _AddressBook;
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;
        private IAddressBookUICommandFactory _CommandFactory;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase1Test2()
        {
            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase1.xml";
            Mock <IConfigurationRoot> MockConfig = new Mock<IConfigurationRoot>();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns("AddressBookUseCase1.xml");
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
            _AddressBook = new AddressBook(MockConfig.Object);            
            this.CreateAddressBookUseCase1();
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("*de*")]
        public void UseCase1(string filter)
        {
            //Arrange
            _InputIterator = new InputIterator(filter, "-1", null, null, null, null, null, null);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);

            //Actions
            IUICommand GetList = _CommandFactory.GetCommand("l");

            //Assert
            Assert.True(GetList.Run().WasSuccessful);
        }

        private void CreateAddressBookUseCase1()
        {
            Contact NewContact;
            Address NewAddress;

            NewContact = new Contact();
            NewContact.Name = "An Dematras";
            NewContact.PhoneNumber = "02/5820103";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "André Hazes";
            NewContact.Email = "andre@heaven.com";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "Jan Franchipan";
            NewContact.Email = "jan@eigenbelang.be";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "Josephine DePin";
            NewContact.Email = "jospin@proximus.be";
            NewContact.PhoneNumber = "054/44.87.26";
            NewAddress = new Address("Weverijstraat 12", "9500", "Geraardsbergen");
            NewContact.Address = NewAddress;
            _AddressBook.Add(NewContact);
            _AddressBook.Save();
        }
    }
}