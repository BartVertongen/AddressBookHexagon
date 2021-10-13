// By Bart Vertongen copyright 2021

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Application.UseCases;
using AddressBook.Hexagon.Application.Services;
using WebAPIAddressBook.Controllers;


namespace WebApiAddressBook.Test
{
    public class GetOverviewControllerTest
    {
        private readonly IAddressBookFile       _DSAddressBook;
        private readonly IGetOverviewQuery      _Service;
        readonly GetOverviewController          _Controller;

        class DSAddressBookMock : IAddressBookFile
        {
            public DSAddressBookMock(IConfigurationRoot config)
            {
                FullPath = config.GetSection("ContactsFile").Value;
            }

            public string FullPath { get; private set; }

            public void Load(AddressBookDTO book)
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

            public void Load(IAddressBookDTO book)
            {
                throw new NotImplementedException();
            }

            public void Save(IList<IContactDTO> book)
            {
            }

            public void Save(IAddressBookDTO book)
            {
                throw new NotImplementedException();
            }
        }

        public GetOverviewControllerTest()
        {
            string sFullPath = Environment.CurrentDirectory + "\\AddressBook.xml";

            Mock<ILogger<UpdateContactController>> StubLogger = new();

            Mock<IConfigurationRoot> StubConfig = new();
            StubConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(sFullPath);

            _DSAddressBook = new DSAddressBookMock(StubConfig.Object);
            _Service = new GetOverviewService(_DSAddressBook);
            _Controller = new GetOverviewController(_Service, StubLogger.Object);
        }

        [Theory]
        [InlineData("", 4)]
        [InlineData("a", 2)]
        [InlineData("*de*", 2)]
        public void GetOverviewController_ShouldGiveOverviewRespectingTheFilter(string filter, int recCount)
        {
            //Arrange 

            //Action
            ActionResult<List<ContactLineDTO>> Result;
            Result = _Controller.GetOverview(filter);

            //Assert
            Assert.Equal(recCount, Result.Value.Count);
        }

        /*[Theory]
        [InlineData("André Hazes")]
        [InlineData("Josephine DePin")]
        public void ControllerGet_ExistingName_ShouldGiveContactWithGivenName(string name)
        {
            //Action
            ActionResult<IContactDTO> Result;
            Result = _Controller.Get(name);

            //Assert
            Assert.Equal(name, Result.Value.Name);
        }

        [Theory]
        [InlineData("Rode Smurf")]
        [InlineData("De Heer onze vader")]
        public void ControllerGet_NotExistingName_ShouldGiveNULL(string name)
        {
            //Action
            ActionResult<IContactDTO> Result;
            Result = _Controller.Get(name);

            //Assert
            Assert.Null(Result.Value);
        }

        [Theory]
        [InlineData("Bart Vertongen", "Remparden 12", "9700", "Oudenaarde", "0470/945806", "bartvertongen70@gmail.com")]
        public void ControllerAdd_AddNonExistingContact_ShouldBeSuccessFul(string name, string street, 
                                                    string postcode, string town, string phone, string email)
        {
            //Arrange
            IContactDTO aContact = new ContactDTO
            {
                Name = name,
                PhoneNumber = phone,
                Email = email,
                Address = new AddressDTO
                {
                    Street = street,
                    PostalCode = postcode,
                    Town = town
                }
            };

            //Action
            IActionResult Result = _Controller.Create(aContact);

            //Assert
            Assert.IsType<CreatedAtActionResult>(Result);
        }

        [Theory]
        [InlineData("Jan Franchipan", "", "", "", "", "jan@eigenbelang.be")]
        public void ControllerAdd_AddExistingContact_ShouldFail(string name, string street,
                                            string postcode, string town, string phone, string email)
        {
            //Arrange
            ContactDTO aContact = new()
            {
                Name = name,
                PhoneNumber = phone,
                Email = email,
                Address = new AddressDTO
                {
                    Street = street,
                    PostalCode = postcode,
                    Town = town
                }
            };

            //Action
            IActionResult Result = _Controller.Create(aContact);

            //Assert
            Assert.IsType<BadRequestObjectResult>(Result);
        }*/
    }
}
