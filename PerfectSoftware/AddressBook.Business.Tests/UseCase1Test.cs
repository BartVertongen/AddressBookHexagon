// By Bart Vertongen copyright 2021

using Microsoft.Extensions.Configuration;
using Moq;
using PS.AddressBook.Data;
using PS.AddressBook.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace PS.AddressBook.Business.Tests
{
    /// <summary>
    /// Give Overview of All Contacts with possible filtering.
    /// </summary>
    public class UseCase1Test
    {
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
                throw new NotImplementedException();
            }
        }

        private readonly AddressBook _AddressBook;
        private List<ContactLineDTO> _ResultList;
        //IList<IContactDTO> _ContactDTOList;
        private string _Filter;

        public UseCase1Test()
        {
            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase1.xml";

            Mock<IConfigurationRoot> MockConfig = new();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(FullPath);

            IDSAddressBook MockDSAddressBook = new DSAddressBookMock(MockConfig.Object);
            _AddressBook = new AddressBook(MockDSAddressBook);
            _Filter = "";
        }

        [Theory]
        [InlineData("", 4)]
        [InlineData("a", 2)]
        [InlineData("*de*", 2)]
        [InlineData("*phi*", 1)]
        public void UseCase1Execute_WithFilter_ShouldGiveResultCount(string filter, int count)
        {
            //Arrange

            //Actions
            this.Step1(filter);
            this.Step2();
            this.Step3();

            //Assert
            Assert.Equal(count, _ResultList.Count);
        }

        /// <summary>
        /// START:TRIGGER: USER asks for an overview of the adress Book Contacts with possible filtering.
        /// </summary>
        private void Step1(string filter)
        {
            //Console.WriteLine($"UseCase1: Give Overview of All Contacts with possible filtering.");
            //Console.Write($"Give in the filter you want to Use: ");
            _Filter = filter;
            //Console.WriteLine();
        }

        /// <summary>
        /// The SYSTEM gets all Contacts from the DB passing the filter.
        /// </summary>
        private void Step2()
        {
            this._ResultList = _AddressBook.GetOverview(_Filter).Cast<ContactLineDTO>().ToList();
        }

        /// <summary>
        /// END: The SYSTEM shows the collected Contacts.
        /// POSTCONDITION: We should have a list of the Contacts filtered by the given filter string.
        /// </summary>
        private void Step3()
        {
            //We can not show anything because there is no user interface
        }
    }
}