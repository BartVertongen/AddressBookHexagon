//Copyright 2021 Bart Vertongen.

using System;
using Xunit;
using PS.AddressBook.Business;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.UI;
using PS.AddressBook.UI.Commands;


namespace UseCaseTests2
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase2Test2 : IDisposable
    {
        private AddressBook _AddressBook;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;
        private IAddressBookUICommandFactory _CommandFactory;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase2Test2()
        {
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase2.xml";
            _Console = new TestConsole();
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
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
            _AddressBook.Load();
            IUICommand AddCommand = _CommandFactory.GetCommand("a");

            //Actions

            //Assert
            Assert.True(AddCommand.Run().WasSuccessful);
        }
    }
}