//Copyright 2021 Bart Vertongen.

using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Framework.Console;
using PS.AddressBook.Framework.Console.Commands;


namespace UseCaseTests
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class ExtendedUseCase2_4ATest
    {
        private readonly ICreateContactUseCase _CreateContactPort;
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;
        private IAddressBookUICommandFactory _CommandFactory;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public ExtendedUseCase2_4ATest()
        {
            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase2.xml";
            Mock<IConfigurationRoot> MockConfig = new();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns("AddressBookUseCase2.xml");
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
            Mock<IAddressBookFile> MockDSAddressBook = new();
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
            //_AddressBook.Clear();
            _InputIterator = new InputIterator(null, "-1", name, null, null, null, phone, email);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_CreateContactPort, null, null, null, null, _UserInterface);
            _CommandFactory.GetCommand("a").Run(out object oResult);

            //Action: Add the same contact
            _InputIterator = new InputIterator(null, "-1", name, null, null, null, phone, email);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_CreateContactPort, null, null, null, null, _UserInterface);
            AddCommand = _CommandFactory.GetCommand("a");

            //Assert
            Assert.False(AddCommand.Run(out oResult).WasSuccessful);
        }
    }
}