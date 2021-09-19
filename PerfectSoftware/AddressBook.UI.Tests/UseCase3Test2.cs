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
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase3Test2
    {
        private AddressBook _AddressBook;
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;
        private IAddressBookUICommandFactory _CommandFactory;
        private readonly IFile _File;
        private readonly Mock<IFile> FileMock;


        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase3Test2()
        {
            //We should not use a real File but Mock it.
            FileMock = new Mock<IFile>();
            FileMock.Setup(f => f.Exists(It.IsAny<String>())).Returns(true);
            FileMock.Setup(f => f.Delete(It.IsAny<String>()));

            _File = FileMock.Object;
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase3.xml";
            if (_File.Exists(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile))
            {
                _File.Delete(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile);
            }
            this.CreateAddressBookUseCase3();

        }

        /// <summary>
        /// UseCase3 Main
        /// </summary>
        [Theory]
        [InlineData("andr","xx", "xx", "xx", "0081780", "andre@hell.com")]
        [InlineData("*demat*", "xx", "xx", "xx", "", "an1989@telenet.be")]
        public void UseCase3_UpdateExistingContact_ShouldGiveChangedContact(string filter, 
                string newStreet, string newPostCode, string newTown, string newPhone, string newEmail)
        {
            //Arrange
            _AddressBook.Load();
            _InputIterator = (IInputIterator)new InputIterator(filter, "-1", null, newStreet, newPostCode, newTown, newPhone, newEmail);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface();
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            IUICommand UpdateCommand = _CommandFactory.GetCommand("u");

            //Actions and Assert
            Assert.True(UpdateCommand.Run().WasSuccessful);
        }


        private void CreateAddressBookUseCase3()
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