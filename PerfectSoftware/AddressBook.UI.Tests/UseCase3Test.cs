//Copyright 2021 Bart Vertongen.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using PS.AddressBook.Hexagon.Domain.Core;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Framework.Console;
using PS.AddressBook.Hexagon.Framework.Console.Commands;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.UI.UseCases
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase3Test
    {
        class DSAddressBookMock : IAddressBookFile
        {
            public DSAddressBookMock(IConfigurationRoot config)
            {
                FullPath = config.GetSection("ContactsFile").Value;
            }

            public string FullPath { get; private set; }

            public void Load(IList<IContactDTO> book)
            {
                IContactDTO NewContact;

                NewContact = new ContactDTO
                {
                    Name = "An Dematras",
                    PhoneNumber = "02/5820103"
                };
                book.Add(NewContact);

                NewContact = new ContactDTO
                {
                    Name = "André Hazes",
                    Email = "andre@heaven.com"
                };
                book.Add(NewContact);

                NewContact = new ContactDTO
                {
                    Name = "Jan Franchipan",
                    Email = "jan@eigenbelang.be"
                };
                book.Add(NewContact);

                NewContact = new ContactDTO
                {
                    Name = "Josephine DePin",
                    Email = "jospin@proximus.be",
                    PhoneNumber = "054/44.87.26",
                    Address = new AddressDTO
                    {
                        Street = "Weverijstraat 12",
                        PostalCode = "9500",
                        Town = "Geraardsbergen"
                    }
                };
                book.Add(NewContact);
            }

            public void Save(IList<IContactDTO> book)
            {
            }
        }

        /// <summary>
        /// This is a class that allows you to set all the future inputs for the Test Console.
        /// Every time you call 'GetInput' the next item is returned.
        /// </summary>
        public class UserInputMock : IInputIterator
        {
            private static int iCounter = 1;
            private readonly string _Filter;
            private bool bFilterProcessed = false;
            private readonly string _Selection;
            private bool bSelectionProcessed = false;
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

            private bool CanGiveFilter()
            {
                if (iCounter == 1 && _Filter != null)
                {
                    iCounter++;
                    bFilterProcessed = true;
                    return true;
                }
                else if (iCounter == 1 && _Filter == null)
                {
                    iCounter++;
                    bFilterProcessed = true;
                    return false;
                }
                else
                    return false;
            }

            private bool CanGiveSelection()
            {
                if (iCounter == 2 && _Selection != "-1")
                {
                    iCounter++;
                    bSelectionProcessed = true;
                    return true;
                }
                else if (iCounter == 2 && _Selection == "-1")
                {
                    iCounter++;
                    bSelectionProcessed = true;
                    return false;
                }
                else
                    return false;
            }

            private bool CanGiveName()
            {
                if (iCounter == 3 && _Name != null)
                {
                    iCounter++;
                    bNameProcessed = true;
                    return true;
                }
                else if (iCounter == 3 && _Name == null)
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
                if (iCounter == 4 && _Street != null)
                {
                    iCounter++;
                    bStreetProcessed = true;
                    return true;
                }
                else if (iCounter == 4 && _Street == null)
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
                if (iCounter == 5 && _PostalCode != null)
                {
                    iCounter++;
                    this.bPostalCodeProcessed = true;
                    return true;
                }
                else if (iCounter == 5 && _PostalCode == null)
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
                if (iCounter == 6 && _Town != null)
                {
                    iCounter++;
                    this.bTownProcessed = true;
                    return true;
                }
                else if (iCounter == 6 && _Town == null)
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
                if (iCounter == 7 && _Phone != null)
                {
                    iCounter++;
                    this.bPhoneProcessed = true;
                    return true;
                }
                else if (iCounter == 7 && _Phone == null)
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
                if (iCounter == 8 && _Email != null)
                {
                    iCounter++;
                    this.bEmailProcessed = true;
                    return true;
                }
                else if (iCounter == 8 && _Email == null)
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
                if (!bFilterProcessed && this.CanGiveFilter())
                    return _Filter;
                else if (!bSelectionProcessed && this.CanGiveSelection())
                    return _Selection;
                else if (!bNameProcessed && this.CanGiveName())
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

        private readonly BussAddressBook _AddressBook;
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private IConsoleUserInterface _UserInterface;
        private IAddressBookUICommandFactory _CommandFactory;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase3Test()
        {
            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase3.xml";
            Mock<IConfigurationRoot> MockConfig = new();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns("AddressBookUseCase3.xml");
            IAddressBookFile MockDSAddressBook = new DSAddressBookMock(MockConfig.Object);
            _AddressBook = new BussAddressBook(MockDSAddressBook);
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
            _InputIterator = new UserInputMock(filter, "1", null, newStreet, newPostCode, newTown, newPhone, newEmail);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(_AddressBook, _UserInterface);
            IUICommand UpdateCommand = _CommandFactory.GetCommand("u");

            //Actions and Assert
            Assert.True(UpdateCommand.Run().WasSuccessful);
        }
    }
}