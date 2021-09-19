//Copyright 2021 Bart Vertongen.

using System;
using System.IO.Abstractions;
using Xunit;
using Moq;
using PS.AddressBook.Business;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.UI.Commands;
using PS.AddressBook.UI;


namespace UseCaseTests2
{
    /// <summary>
    /// The Use Case to Delete a Contact.
    /// </summary>
    /// <remarks>
    /// The first step is the trigger: the user initiates the delete.
    /// In fact this is done by starting the UseCase4Test.
    /// </remarks>
    public class UseCase4Test2
    {
        private AddressBook _AddressBook;
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private ConsoleUserInterface _UserInterface;
        private IAddressBookUICommandFactory _CommandFactory;
        private IFile _File;
        private readonly Mock<IFile> FileMock;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase4Test2()
        {
            //We should not use a real File but Mock it.
            FileMock = new Mock<IFile>();
            FileMock.Setup(f => f.Exists(It.IsAny<String>())).Returns(true);
            FileMock.Setup(f => f.Delete(It.IsAny<String>()));

            _File = FileMock.Object;
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase4.xml";
            if (_File.Exists(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile))
            {
                _File.Delete(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile);
            }
            this.CreateAddressBookUseCase4();
        }

        [Theory]
        [InlineData("jan", 1)]
        [InlineData("*de*", 1)]
        public void UseCase4(string filter, int selectedContact)
        {
            //Arrange
            _AddressBook.Load();
            _InputIterator = (IInputIterator)new InputIterator(filter, null, null, null, null, null, null);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            IUICommand DeleteCommand = _CommandFactory.GetCommand("d");

            //Actions and Assert
            Assert.True(DeleteCommand.Run().WasSuccessful);
        }


        private void CreateAddressBookUseCase4()
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