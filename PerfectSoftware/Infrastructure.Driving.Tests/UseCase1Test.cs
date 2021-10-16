//Copyright 2021 Bart Vertongen.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Framework.Console;
using PS.AddressBook.Framework.Console.Commands;


namespace PS.AddressBook.UI.UseCases
{
    /// <summary>
    /// Give Overview of All Contacts with possible filtering
    /// </summary>
    public class UseCase1Test
    {
        private IInputIterator _InputIterator;
        private IConsole _Console;
        private IConsoleUserInterface   _UserInterface;
        private IGetOverviewQuery       _GetOverviewPort;
        private IAddressBookUICommandFactory _CommandFactory;

        class DALAddressBookMock : IAddressBookFile
        {
            public DALAddressBookMock(IConfigurationRoot config)
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
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase1Test()
        {
            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase1.xml";

            Mock <IConfigurationRoot> MockConfig = new();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(FullPath);

            IAddressBookFile MockDALAddressBook = new DALAddressBookMock(MockConfig.Object);
           
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("*de*")]
        public void UseCase1(string filter)
        {
            //Arrange
            _InputIterator = new InputIterator(filter, "-1", null, null, null, null, null, null);
            _Console = new TestConsole(_InputIterator);
            _UserInterface = new ConsoleUserInterface(_Console);
            _CommandFactory = new AddressBookUICommandFactory(null, null, null, _GetOverviewPort,null, _UserInterface);

            //Actions
            IUICommand GetList = _CommandFactory.GetCommand("l");

            //Assert
            Assert.True(GetList.Run(out object oResult).WasSuccessful);
        } 
    }
}