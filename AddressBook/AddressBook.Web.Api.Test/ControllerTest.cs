// By Bart Vertongen copyright 2021

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Services;
using WebAPIAddressBook.Controllers;


namespace WebApiAddressBook.Test
{
    public class GetOverviewControllerTest
    {
        private readonly IAddressBookFile       _DALAddressBook;
        private readonly IGetOverviewQuery      _GetOverviewPort;
        readonly GetOverviewController          _Controller;

        class DALAddressBookFileMock : IAddressBookFile
        {
            public DALAddressBookFileMock(IConfigurationRoot config)
            {
                FullPath = config.GetSection("ContactsFile").Value;
            }

            public string FullPath { get; private set; }

            public void Load(IAddressBookDTO book)
            {
                IContactDTO NewContact;

                NewContact = new ContactDTO
                {
                    Name = "An Dematras",
                    Phone = "02/5820103"
                };
                (book as IList<IContactDTO>).Add(NewContact);

                NewContact = new ContactDTO
                {
                    Name = "André Hazes",
                    Email = "andre@heaven.com"
                };
                (book as IList<IContactDTO>).Add(NewContact);

                NewContact = new ContactDTO
                {
                    Name = "Jan Franchipan",
                    Email = "jan@eigenbelang.be"
                };
                (book as IList<IContactDTO>).Add(NewContact);

                NewContact = new ContactDTO
                {
                    Name = "Josephine DePin",
                    Email = "jospin@proximus.be",
                    Phone = "054/44.87.26",
                    Address = new AddressDTO
                    {
                        Street = "Weverijstraat 12",
                        PostalCode = "9500",
                        Town = "Geraardsbergen"
                    }
                };
                (book as IList<IContactDTO>).Add(NewContact);
            }

            public void Save(IAddressBookDTO book)
            {
            }
        }

        public GetOverviewControllerTest()
        {
            string sFullPath = Environment.CurrentDirectory + "\\AddressBook.xml";

            Mock<ILogger<UpdateContactController>> StubLogger = new();

            Mock<IConfigurationRoot> StubConfig = new();
            StubConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(sFullPath);

            _DALAddressBook = new DALAddressBookFileMock(StubConfig.Object);
            _GetOverviewPort = new GetOverviewService(_DALAddressBook);
            _Controller = new GetOverviewController(_GetOverviewPort, StubLogger.Object);
        }

        [Theory]
        [InlineData("", 4)]
        [InlineData("a", 2)]
        [InlineData("*de*", 2)]
        public void GetOverviewController_ShouldGiveOverviewRespectingTheFilter(string filter, int recCount)
        {
            //Arrange 

            //Action
            ActionResult<List<IContactLineDTO>> Result;
            Result = _Controller.GetOverview(filter);

            //Assert
            Assert.Equal(recCount, Result.Value.Count);
        }
    }
}
