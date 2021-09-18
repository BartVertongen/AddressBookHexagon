//Copyright 2021 Bart Vertongen.

using System;
using System.IO;
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
        private AddressBook _AddressBook;
        private Contact _ContactFirst, _ContactDouble;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public ExtendedUseCase2_4ATest2()
        {
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase2_4A.xml";
            _ContactFirst = new Contact(_AddressBook);
            _ContactDouble = new Contact(_AddressBook);
            _Console = new TestConsole();
            _UserInterface = new ConsoleUserInterface();
        }


        /// <summary>
        /// Extended UseCase2_4A: The Name given by the User for New Contact exists already
        /// </summary>
        [Theory]
        [InlineData("Anthony Hopkins", "+3202530014", "AHopkins@stars.com")]
        [InlineData("David Deschepper", "+3209/45.14.81", "")]
        public void UseCase2_4A_CreationWithExistingName_ShouldFail(string name, string phone, string email)
        {
            //Arrange
            _ContactFirst.Name = name;
            _ContactFirst.PhoneNumber = phone;
            _ContactFirst.Email = email;
            new AddContactCommand(_AddressBook, _UserInterface).Run();

            //Action
            //Step1: USER trigger the adding a new Contact.
            // This is done by starting the Unit Test.

            //Step2 SYSTEM Asks for Input of the Name
            _Console.Write("Give in a Unique name for the new Contact: ");

            //Step3: USER supplies a Name
            //Step4: SYSTEM will validate the Name
            Action testCode = () => _ContactDouble.Name = _Console.ReadLine();

            TestConsole.UserInput = name;
            //Here we execute the testCode
            var ex = Record.Exception(testCode);
            _Console.WriteLine();

            //Assert
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
        }
    }
}