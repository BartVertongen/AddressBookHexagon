﻿//By Bart Vertongen copyright 2021.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Domain.Core;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.Business.Tests
{
    /// <summary>
    /// UseCase4: Delete a Contact.
    /// </summary>
    public class UseCase4Test
    {
        private readonly IAddressBook _AddressBook;
        private List<ContactLineDTO> _ResultList;
        private string _Filter, _SelectedName;

        public UseCase4Test()
        {
            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase4.xml";
            Mock<IConfigurationRoot> MockConfig = new();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns("AddressBookUseCase4.xml");

            Mock<IAddressBookFile> MockDSAddressBook = new();
            _AddressBook = new BussAddressBook(MockDSAddressBook.Object);
            this.PreCondition();
        }

        [Theory]
        [InlineData("a", 1)]
        [InlineData("*de*", 1)]
        [InlineData("*phi*", 1)]
        public void UseCase4Execute_DeleteOneContact_ShouldHaveCount3(string filter, int selection)
        {
            //Arrange

            //Action
            this.Step1(filter);
            this.Step2();
            this.Step3();
            this.Step4(selection);
            this.Step5();
            this.Step6();

            //Assert
            Assert.Equal(3, _AddressBook.Count);
        }

        /// <summary>
        /// You need an AddressBook with Contacts.
        /// </summary>
        private void PreCondition()
        {
            

            this.CreateAddressBookUseCase4();
            _Filter = "";
        }

        /// <summary>
        /// START:TRIGGER: USER asks for an overview of the adress Book Contacts with possible filtering.
        /// STEP1: 
        /// </summary>
        private void Step1(string filter)
        {
            //Console.WriteLine($"UseCase4: Delete a Contact.");
            //Console.Write($"Give in the filter you want to select the Contact: ");
            _Filter = filter;
        }

        /// <summary>
        /// The SYSTEM gets all Contacts from the DB passing the filter.
        /// </summary>
        private void Step2()
        {          
            this._ResultList = _AddressBook.GetOverview(_Filter).Cast<ContactLineDTO>().ToList();
        }

        /// <summary>
        /// STEP3: The System shows all possible Contacts.
        /// </summary>
        private void Step3()
        {
        }

        /// <summary>
        /// STEP4: The USER selects the Contact he wants to Delete.
        /// </summary>
        private void Step4(int id)
        {
            this._SelectedName = this._ResultList[id-1].Name;               
        }

        /// <summary>
        /// STEP 5:  The SYSTEM adapts the AddressBook, does the Delete.
        /// </summary>
        private void Step5()
        {
            if (!string.IsNullOrEmpty(this._SelectedName))
            {
                this._AddressBook.Delete(this._SelectedName);
            }
        }

        /// <summary>
        /// STEP 6:  The System removes the Contact from the DB.
        /// </summary>
        private void Step6()
        {
            if (!string.IsNullOrEmpty(this._SelectedName))
                this._AddressBook.Save();
        }

        private void CreateAddressBookUseCase4()
        {
            Contact NewContact;
            Address NewAddress;

            NewContact = new Contact
            {
                Name = "An Dematras",
                PhoneNumber = "02/5820103"
            };
            _AddressBook.Add(NewContact);

            NewContact = new Contact
            {
                Name = "André Hazes",
                Email = "andre@heaven.com"
            };
            _AddressBook.Add(NewContact);

            NewContact = new Contact
            {
                Name = "Jan Franchipan",
                Email = "jan@eigenbelang.be"
            };
            _AddressBook.Add(NewContact);

            NewContact = new Contact
            {
                Name = "Josephine DePin",
                Email = "jospin@proximus.be",
                PhoneNumber = "054/44.87.26"
            };
            NewAddress = new Address("Weverijstraat 12", "9500", "Geraardsbergen");
            NewContact.Address = NewAddress;
            _AddressBook.Add(NewContact);

            _AddressBook.Save();
        }
    }
}