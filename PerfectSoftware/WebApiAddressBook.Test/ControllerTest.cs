
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using PS.AddressBook.Business;
using PS.AddressBook.Data;
using PS.AddressBook.Data.Interfaces;
using System;
using System.Collections.Generic;
using WebAPIAddressBook.Controllers;


namespace WebApiAddressBook.Test
{
    public class ControllerTest
    {
        IDSAddressBook          _DSAddressBook;
        IAddressBook            _AddressBook;
        IAddressBookService     _Service;
        AddressBookController _Controller;

        class DSAddressBookMock : IDSAddressBook
        {
            public DSAddressBookMock(IConfigurationRoot config)
            {
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
            }
        }

        public ControllerTest()
        {
            string sFullPath = Environment.CurrentDirectory + "\\AddressBook.xml";

            Mock<ILogger<AddressBookController>> StubLogger = new();

            Mock<IConfigurationRoot> StubConfig = new();
            StubConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(sFullPath);

            _DSAddressBook = new DSAddressBookMock(StubConfig.Object);
            _AddressBook = new AddressBook(_DSAddressBook);
            _Service = new AddressBookService(_AddressBook);
            _Controller = new AddressBookController(_Service, StubLogger.Object);
        }

        [Theory]
        [InlineData("", 4)]
        [InlineData("a", 2)]
        [InlineData("*de*", 2)]
        public void ControllerGetOverview_ShouldGiveOverviewRespectingTheFilter(string filter, int recCount)
        {
            //Action
            ActionResult<List<ContactLineDTO>> Result;
            Result = _Controller.GetOverview(filter);

            //Assert
            Assert.Equal(recCount, Result.Value.Count);
        }

        [Theory]
        [InlineData("André Hazes")]
        [InlineData("Josephine DePin")]
        public void ControllerGet_ExistingName_ShouldGiveContactWithGivenName(string name)
        {
            //Action
            ActionResult<ContactDTO> Result;
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
            ActionResult<ContactDTO> Result;
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
            ContactDTO aContact = new ContactDTO
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
            ContactDTO aContact = new ContactDTO
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
        }
    }
}
