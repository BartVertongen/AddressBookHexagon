//Copyright 2021 Bart Vertongen.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Framework.Console;
using PS.AddressBook.Hexagon.Framework.Console.Commands;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.UI.UseCases
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase2Test : IDisposable
    {
        private readonly BussAddressBook _AddressBook;
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;
        private IAddressBookUICommandFactory _CommandFactory;

        /// <summary>
        /// This is a class that allows you to set all the future inputs for the Test Console.
        /// Every time you call 'GetInput' the next item is returned.
        /// </summary>
        public class UserInputMock : IInputIterator
        {
            private static int iCounter = 1;
            private readonly string _Filter;
            private readonly bool bFilterProcessed = false;
            private readonly string _Selection;
            private readonly bool bSelectionProcessed = false;
            private readonly string _Name;
            private bool bNameProcessed = false;
            private readonly string _Street;
            private bool bStreetProcessed = false;
            private readonly string _PostalCode;
            private bool bPostalCodeProcessed = false;
            private readonly string _Town;
            private bool bTownProcessed = false;
            private readonly string _Phone;
            private bool bPhoneProcessed = false;
            private readonly string _Email;
            private bool bEmailProcessed = false;

            public UserInputMock(string filter, string selection,
                    string name, string street, string postalcode, string town, string phone, string email)
            {
                iCounter = 1;
                _Filter = filter;
                _Selection = selection;
                _Name = name;
                _Street = street;
                _PostalCode = postalcode;
                _Town = town;
                _Phone = phone;
                _Email = email;
            }

            private bool CanGiveName()
            {
                if (iCounter == 1 && _Name != null)
                {
                    iCounter++;
                    bNameProcessed = true;
                    return true;
                }
                else if (iCounter == 1 && _Name == null)
                {
                    iCounter++;
                    bNameProcessed = true;
                    return false;
                }
                else
                    return false;
            }

            private bool CanGiveStreet()
            {
                if (iCounter == 2 && _Street != null)
                {
                    iCounter++;
                    bStreetProcessed = true;
                    return true;
                }
                else if (iCounter == 2 && _Street == null)
                {
                    iCounter++;
                    bStreetProcessed = true;
                    return false;
                }
                else
                    return false;
            }

            private bool CanGivePostalCode()
            {
                if (iCounter == 3 && _PostalCode != null)
                {
                    iCounter++;
                    this.bPostalCodeProcessed = true;
                    return true;
                }
                else if (iCounter == 3 && _PostalCode == null)
                {
                    iCounter++;
                    this.bPostalCodeProcessed = true;
                    return false;
                }
                else
                    return false;
            }

            private bool CanGiveTown()
            {
                if (iCounter == 4 && _Town != null)
                {
                    iCounter++;
                    this.bTownProcessed = true;
                    return true;
                }
                else if (iCounter == 4 && _Town == null)
                {
                    iCounter++;
                    this.bTownProcessed = true;
                    return false;
                }
                else
                    return false;
            }

            private bool CanGivePhone()
            {
                if (iCounter == 5 && _Phone != null)
                {
                    iCounter++;
                    this.bPhoneProcessed = true;
                    return true;
                }
                else if (iCounter == 5 && _Phone == null)
                {
                    iCounter++;
                    this.bPhoneProcessed = true;
                    return false;
                }
                else
                    return false;
            }

            private bool CanGiveEmail()
            {
                if (iCounter == 6 && _Email != null)
                {
                    iCounter++;
                    this.bEmailProcessed = true;
                    return true;
                }
                else if (iCounter == 6 && _Email == null)
                {
                    iCounter++;
                    this.bEmailProcessed = true;
                    return true;
                }
                else
                    return false;
            }

            public string GetInput()
            {
                if (!bNameProcessed && this.CanGiveName())
                    return _Name;
                else if (!bStreetProcessed && this.CanGiveStreet())
                    return _Street;
                else if (!bPostalCodeProcessed && this.CanGivePostalCode())
                    return _PostalCode;
                else if (!bTownProcessed && this.CanGiveTown())
                    return _Town;
                else if (!bPhoneProcessed && this.CanGivePhone())
                    return _Phone;
                else if (!bEmailProcessed && this.CanGiveEmail())
                    return _Email;
                else
                    return null;
            }
        }

        class DSAddressBookMock : IAddressBookFile
        {
            public DSAddressBookMock(IConfigurationRoot config)
            {
                FullPath = config.GetSection("ContactsFile").Value;
            }

            public string FullPath { get; private set; }

            public void Load(IAddressBookDTO book)
            {
                //Nothing to load, we start from an empty one.
            }

            public void Save(IAddressBookDTO book)
            {
                //We save nothing this is just for testing
            }
        }

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase2Test()
        {
            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase2.xml";
            Mock<IConfigurationRoot> MockConfig = new();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(FullPath);
            IAddressBookFile MockDSAddressBook = new DSAddressBookMock(MockConfig.Object);
            _AddressBook = new BussAddressBook(MockDSAddressBook);
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
            _InputIterator = new UserInputMock(null, "-1", name, street, postalcode, town, phone, email);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            IUICommand AddCommand = _CommandFactory.GetCommand("a");

            //Action and Assert
            Assert.True(AddCommand.Run().WasSuccessful);
        }
    }
}