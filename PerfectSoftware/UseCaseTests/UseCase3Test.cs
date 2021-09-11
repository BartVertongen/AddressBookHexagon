//Copyright 2021 Bart Vertongen.

using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using PS.AddressBook.Business;
using System.Linq;

namespace UseCaseTests
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase3Test
    {
        private AddressBook _AddressBook;
        private Contact _Contact;
        private List<ContactLineDTO> _ResultList;
        private string _Filter = "";

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase3Test()
        {            
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase3.xml";
            _Contact = new Contact(_AddressBook);
            if (File.Exists(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile))
            {
                File.Delete(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile);
            }
            this.CreateAddressBookUseCase3();
            _Filter = "";
        }

        /// <summary>
        /// UseCase3 Main
        /// </summary>
        /// <remarks>
        /// Starting this test is equal to trigger for Contact update.
        /// So this is Step 1.
        /// </remarks>
        [Fact]
        public void UseCase3_UpdateExistingContact_ShouldGiveChangedContact()
        {
            //Arrange
            _AddressBook.Load();

            //Actions
            this.Step2And3("Jose");
            this.Step4();
            this.Step5();
            this.Step6();
            this.Step7();
            this.Step8();
            this.Step9();
            this.Step10();
            _AddressBook.Load();

            //Assert
            Assert.True(_AddressBook.Count == 4);
        }


        /// <summary>
        /// Step2: The Systems aks for a Filter to show an Overview of Contacts to choose from.
        /// Step3: The User supplies the Filter.
        /// </summary>
        private void Step2And3(string filter)
        {
            this._Filter = filter;
        }

        /// <summary>
        /// The System collects the Contacts passing the filter and shows them.
        /// </summary>
        private void Step4()
        {
            this._ResultList = _AddressBook.GetOverview(_Filter).Cast<ContactLineDTO>().ToList();
            Assert.Single(this._ResultList);
        }

        /// <summary>
        /// The User Selects the Contact he wants to Update.
        /// </summary>
        private void Step5()
        {
            Assert.True(this._ResultList[0].Name == "Josephine DePin");
        }

        /// <summary>
        /// The System Retrieves the Contact with that Name.
        /// </summary>
        private void Step6()
        {
            _Contact = (Contact)_AddressBook.GetContact(this._ResultList[0].Name);
        }

        /// <summary>
        /// Use Case 3.7 Update the Adress of the Contact.
        /// </summary>
        private void Step7()
        {
            Address bussAddress = new Address();

            UseCase3_7Test UseCase3_7 = new UseCase3_7Test();
            bussAddress.Street = _Contact.Address.Street;
            bussAddress.PostalCode = _Contact.Address.PostalCode;
            bussAddress.Town = _Contact.Address.Town;
            UseCase3_7.Address = bussAddress;
            UseCase3_7.UseCase3_7_CreationValidAdress_GivesFullAddress();
            _Contact.Address = UseCase3_7.Address;
        }

        /// <summary>
        /// Use Case3.8 to Update the PhoneNumber
        /// </summary>
        /// <param name="phone"></param>
        private void Step8()
        {
            UseCase3_8Test UseCase3_8 = new UseCase3_8Test();
            UseCase3_8.Contact = _Contact;
            UseCase3_8.UseCase3_8_UpdateWith_ValidPhoneNumber();
            _Contact.PhoneNumber = UseCase3_8.Contact.PhoneNumber;
        }

        /// <summary>
        /// Use Case3.9 to Update the Email
        /// </summary>
        /// <param name="email"></param>
        private void Step9()
        {
            UseCase3_9Test UseCase3_9 = new UseCase3_9Test();
            UseCase3_9.Contact = _Contact;
            UseCase3_9.UseCase3_9_UpdateWith_ValidEmail();
            _Contact.Email = UseCase3_9.Contact.Email;
        }

        /// <summary>
        /// The System will Update the changed Contact to the AdressBook and Xml.
        /// </summary>
        private void Step10()
        {
            //Update Contact in the AddressBook in memory
            _AddressBook.Update(_Contact);
            //Add the change to Xml AddressBook
            _AddressBook.Save();
        }

        private void CreateAddressBookUseCase3()
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