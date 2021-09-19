//Copyright 2021 Bart Vertongen.

using Xunit;
using PS.AddressBook.Business;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.UI.Commands;
using PS.AddressBook.UI;


namespace UseCaseTests2
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase2_5_2Test2
    {
        private readonly AddressBook _AddressBook;
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;
        private IAddressBookUICommandFactory _CommandFactory;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase2_5_2Test2()
        {
            _AddressBook = new AddressBook(null)
            {
                XmlFile = "AddressBookUseCase2.xml"
            };
        }

        /// <summary>
        /// UseCase2 Main
        /// </summary>
        /// <remarks>
        /// Starting this test is equal to trigger for Contact creation.
        /// So this is Step 1.
        /// </remarks>
        [Theory]
        [InlineData("Jan Franchipan", "", "9500", "Geraardsbergen", "+3254/48.72.49", "janfranchi@telenet.be")]
        [InlineData("Jan Franchipan", "Weverijstraat 12", "", "Geraardsbergen", "+3254/48.72.49", "janfranchi@telenet.be")]
        [InlineData("Jan Franchipan", "Weverijstraat 12", "9500", "", "+3254/48.72.49", "janfranchi@telenet.be")]
        public void UseCase2_5_2_CreationAdressWithMissingData_ShouldGiveEmptyAddress(string name, string street, 
                                                            string postalcode, string town, string phone, string email)
        {
            //Arrange
            _AddressBook.Load();
            _InputIterator = (IInputIterator)new InputIterator(null, name, street, postalcode, town, phone, email);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface();
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            IUICommand AddCommand = _CommandFactory.GetCommand("a");

            //TODO select the new added Contact

            //TODO show it.

            //Action and Assert
            Assert.False(AddCommand.Run().WasSuccessful);
        }
    }
}