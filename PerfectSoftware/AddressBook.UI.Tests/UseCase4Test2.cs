//Copyright 2021 Bart Vertongen.

using System;
using System.IO;
using System.IO.Abstractions;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.UI.Commands;
using PS.AddressBook.UI;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


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
        private BussAddressBook _AddressBook;
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;
        private IAddressBookUICommandFactory _CommandFactory;
        /*private IFile _File;
        private readonly Mock<IFile> FileMock;*/

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase4Test2()
        {
            //We should not use a real File but Mock it.
            /*FileMock = new Mock<IFile>();
            FileMock.Setup(f => f.Exists(It.IsAny<String>())).Returns(true);
            FileMock.Setup(f => f.Delete(It.IsAny<String>()));
            _File = FileMock.Object;*/

            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase4.xml";
            Mock<IConfigurationRoot> MockConfig = new Mock<IConfigurationRoot>();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns("AddressBookUseCase4.xml");
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
            _AddressBook = new BussAddressBook(MockConfig.Object);
        }

        [Theory]
        [InlineData("jan", "1")]
        [InlineData("*de*", "1")]
        public void UseCase4(string filter, string selectedContact)
        {
            //Arrange
            if (_AddressBook.Count == 0)
            {
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
            }

            _InputIterator = new InputIterator(filter, selectedContact, null, null, null, null, null, null);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            IUICommand DeleteCommand = _CommandFactory.GetCommand("d");

            //Actions and Assert
            Assert.True(DeleteCommand.Run().WasSuccessful);
        }
    }
}