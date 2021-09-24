// By Bart Vertongen copyright 2021

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;


namespace PS.AddressBook.Business.Tests
{
    /// <summary>
    /// Give Overview of All Contacts with possible filtering.
    /// </summary>
    public class UseCase1Test
    {
        private AddressBook _AddressBook;
        private List<ContactLineDTO> _ResultList;
        private string _Filter;

        public UseCase1Test()
        {
            this.PreCondition();
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
            Assert.Equal(count, _ResultList.Count());
        }

        /// <summary>
        /// You need an AddressBook with Contacts.
        /// </summary>
        private void PreCondition()
        {
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase1.xml";
            if (File.Exists(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile))
            {
                File.Delete(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile);
            }
            this.CreateAddressBookUseCase1();
            _Filter = "";
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

        private void CreateAddressBookUseCase1()
        {
            Contact NewContact;
            Address NewAddress;

            NewContact = new Contact();
            NewContact.Name = "An Dematras";
            NewContact.PhoneNumber = "02/5820103";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "André Hazes";
            NewContact.Email = "andre@heaven.com";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "Jan Franchipan";
            NewContact.Email = "jan@eigenbelang.be";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "Josephine DePin";
            NewContact.Email = "jospin@proximus.be";
            NewContact.PhoneNumber = "054/44.87.26";
            NewAddress = new Address("Weverijstraat 12", "9500", "Geraardsbergen");
            NewContact.Address = NewAddress;
            _AddressBook.Add(NewContact);

            _AddressBook.Save();
        }
    }
}