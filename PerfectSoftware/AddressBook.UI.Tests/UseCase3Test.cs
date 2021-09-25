//Copyright 2021 Bart Vertongen.

using System;
using System.IO.Abstractions;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using PS.AddressBook.Data.Interfaces;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.UI.Commands;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


namespace PS.AddressBook.UI.UseCases
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase3Test
    {
        private readonly BussAddressBook _AddressBook;
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;
        private IAddressBookUICommandFactory _CommandFactory;
        private readonly IFile _File;
        private readonly Mock<IFile> FileMock;


        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase3Test()
        {
            //We should not use a real File but Mock it.
            FileMock = new Mock<IFile>();
            FileMock.Setup(f => f.Exists(It.IsAny<String>())).Returns(true);
            FileMock.Setup(f => f.Delete(It.IsAny<String>()));
            _File = FileMock.Object;

            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase3.xml";
            Mock<IConfigurationRoot> MockConfig = new Mock<IConfigurationRoot>();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns("AddressBookUseCase3.xml");
            if (_File.Exists(FullPath))
            {
                _File.Delete(FullPath);
            }
            Mock<IDSAddressBook> MockDSAddressBook = new Mock<IDSAddressBook>();
            _AddressBook = new BussAddressBook(MockDSAddressBook.Object);
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
            _AddressBook.Clear();
            _InputIterator = new InputIterator(null, "-1", "An Dematras", "", "", "", "02/5820103", "");
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            _CommandFactory.GetCommand("a").Run();

            _InputIterator = new InputIterator(null, "-1", "André Hazes", "", "", "", "", "andre@heaven.com");
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            _CommandFactory.GetCommand("a").Run();

            _InputIterator = new InputIterator(null, "-1", "Jan Franchipan", "", "", "", "", "jan@eigenbelang.be");
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            _CommandFactory.GetCommand("a").Run();

            _InputIterator = new InputIterator(null, "-1", "Josephine DePin", "Weverijstraat 12", "9500", "Geraardsbergen", "054/44.87.26", "jospin@proximus.be");
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            _CommandFactory.GetCommand("a").Run();


            _InputIterator = new InputIterator(filter, "1", null, newStreet, newPostCode, newTown, newPhone, newEmail);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            IUICommand UpdateCommand = _CommandFactory.GetCommand("u");

            //Actions and Assert
            Assert.True(UpdateCommand.Run().WasSuccessful);
        }
    }
}