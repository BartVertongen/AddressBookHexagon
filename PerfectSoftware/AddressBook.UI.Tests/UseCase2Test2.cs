//Copyright 2021 Bart Vertongen.

using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.UI;
using PS.AddressBook.UI.Commands;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


namespace UseCaseTests2
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase2Test2 : IDisposable
    {
        private BussAddressBook _AddressBook;
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;
        private IAddressBookUICommandFactory _CommandFactory;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase2Test2()
        {
            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase2.xml";
            Mock<IConfigurationRoot> MockConfig = new Mock<IConfigurationRoot>();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns("AddressBookUseCase2.xml");
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
            _AddressBook = new BussAddressBook(MockConfig.Object);
        }

        /// <summary>
        /// The cleanup code.
        /// </summary>
        public void Dispose()
        {
            _AddressBook.Clear();
        }

        [Theory]
        [InlineData("Anthony Hopkins", "", "", "", "+3202530014", "AHopkins@stars.com")]
        [InlineData("Jan Franchipan", "Weverijstraat 12", "9500", "Geraardsbergen", "+3254/48.72.49", "janfranchi@telenet.be")]
        [InlineData("An Delmare", "", "", "", "", "an_weetal@proximus.be")]
        [InlineData("David Deschepper", "", "", "", "+3209/45.14.81", "")]
        public void UseCase2Main_ValidData_ShouldBeSuccessful(string name, string street, 
                                        string postalcode, string town, string phone, string email)
        {
            //Arrange
            _InputIterator = new InputIterator(null, "-1", name, street, postalcode, town, phone, email);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            IUICommand AddCommand = _CommandFactory.GetCommand("a");

            //Action and Assert
            Assert.True(AddCommand.Run().WasSuccessful);
        }
    }
}