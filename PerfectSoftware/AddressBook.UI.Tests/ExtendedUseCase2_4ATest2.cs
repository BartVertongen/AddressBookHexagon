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
    public class ExtendedUseCase2_4ATest2
    {
        private readonly AddressBook _AddressBook;
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public ExtendedUseCase2_4ATest2()
        {
            _AddressBook = new AddressBook
            {
                XmlFile = "AddressBookUseCase2_4A.xml"
            };
        }


        /// <summary>
        /// Extended UseCase2_4A: The Name given by the User for New Contact exists already
        /// </summary>
        [Theory]
        [InlineData("Anthony Hopkins", "+3202530014", "AHopkins@stars.com")]
        [InlineData("David Deschepper", "+3209/45.14.81", "")]
        public void UseCase2_4A_CreationWithExistingName_ShouldFail(string name, string phone, string email)
        {
            //Arrange: add a Contact
            IUICommand AddCommand;
            _InputIterator = (IInputIterator)new InputIterator(null, name, null, null, null, phone, email);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            AddCommand = new AddContactCommand(_AddressBook, _UserInterface);
            AddCommand.Run();

            //Action: Add the same contact
            _InputIterator = (IInputIterator)new InputIterator(null, name, null, null, null, phone, email);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            AddCommand = new AddContactCommand(_AddressBook, _UserInterface);

            //Assert
            Assert.False(AddCommand.Run().WasSuccessful);
        }
    }
}