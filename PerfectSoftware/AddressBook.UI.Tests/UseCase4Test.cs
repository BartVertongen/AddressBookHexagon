//Copyright 2021 Bart Vertongen.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using PS.AddressBook.Data;
using PS.AddressBook.Data.Interfaces;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.UI.Commands;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


namespace PS.AddressBook.UI.UseCases
{
    /// <summary>
    /// The Use Case to Delete a Contact.
    /// </summary>
    /// <remarks>
    /// The first step is the trigger: the user initiates the delete.
    /// In fact this is done by starting the UseCase4Test.
    /// </remarks>
    public class UseCase4Test
    {
        // InputGenerator uses a static and that can give problems in Unit Tests.
        class MockInputIterator : IInputIterator
        {
            private static int iCounter = 1;
            private readonly string _Filter;
            private bool bFilterProcessed = false;
            private readonly string _Selection;
            private bool bSelectionProcessed = false;

            public MockInputIterator(string filter, string selection)
            {
                iCounter = 1;
                _Filter = filter;
                _Selection = selection;
            }

            public string GetInput()
            {
                if (!bFilterProcessed && this.CanGiveFilter())
                    return _Filter;
                else if (!bSelectionProcessed && this.CanGiveSelection())
                    return _Selection;
                else
                    return null;
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

        }

        class DSAddressBookMock : IDSAddressBook
        {
            public DSAddressBookMock(IConfigurationRoot config = null)
            {
                if (config == null)
                    FullPath = "AddressBook.xml";
                else
                    FullPath = config.GetSection("ContactsFile").Value;
            }

            public string FullPath { get; private set; }

            public void Load(IList<IContactDTO> book)
            {
                ContactDTO NewContact;

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
                //We save nothing this is just for testing
            }
        }

        [Theory]
        [InlineData("jan", "1")]
        [InlineData("*de*", "1")]
        public void UseCase4(string filter, string selectedContact)
        {
            //Arrange
            Mock<IConfigurationRoot> aMockConfig;
            IInputIterator anInputIterator;
            IConsole aConsole;
            IConsoleUserInterface anUserInterface;
            IAddressBookUICommandFactory aCommandFactory;
            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase4.xml";
            aMockConfig = new Mock<IConfigurationRoot>();
            aMockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(FullPath);
            DSAddressBookMock aMockDSAddressBook = new DSAddressBookMock();
            BussAddressBook anAddressBook = new BussAddressBook(aMockDSAddressBook);
            anInputIterator = new MockInputIterator(filter, selectedContact);
            aConsole = new TestConsole(anInputIterator);
            anUserInterface = new ConsoleUserInterface(aConsole);
            aCommandFactory = new AddressBookUICommandFactory(anAddressBook, anUserInterface);
            IUICommand DeleteCommand = aCommandFactory.GetCommand("d");

            //Actions and Assert
            Assert.True(DeleteCommand.Run().WasSuccessful);
        }
    }
}