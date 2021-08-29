//Copyright 2021 Bart Vertongen.

using System;
using System.IO;
using System.Collections.Generic;
using Xunit;
using AddressBookLib;


namespace UseCaseTests
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
        private AddressBook _AddressBook;
        private List<ContactLine> _ResultList;
        private string _Filter = "";

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase4Test()
        {
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase4.xml";
            if (File.Exists(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile))
            {
                File.Delete(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile);
            }
            this.CreateAddressBookUseCase4();
            _Filter = "";           
        }

        /// <summary>
        /// UseCase4 Check AddressBook and XML after Delete.
        /// </summary>
        [Fact]
        public void UseCase4_DeleteJan_ShouldNotBeInAddressBookOrXML()
        {
            //Arrange
            _AddressBook.Load();
                
            //Actions
            this.Step2And3("Jan");
            this.Step4();
            this.Step5();
            this.Step6();
            this.Step7();

            //Assert AddressBook in memory
            Assert.True(_AddressBook.Count == 3);
            //Assert XML of AddressBook
            _AddressBook.Load();
            Assert.True(_AddressBook.Count == 3);
            this._ResultList = _AddressBook.GetOverview(_Filter);
            Assert.True(this._ResultList.Count == 0);
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
            this._ResultList = _AddressBook.GetOverview(_Filter);
            Assert.Single(this._ResultList);
        }

        /// <summary>
        /// The User Selects the Contact he wants to Delete.
        /// </summary>
        private void Step5()
        {
            Assert.True(this._ResultList[0].Name == "Jan Franchipan");
        }

        /// <summary>
        /// The System adapts the adress Book.
        /// </summary>
        private void Step6()
        {
            this._AddressBook.Delete(this._ResultList[0].Name);
        }

        /// <summary>
        /// The System removes the chosen Contact from the DB.
        /// </summary>
        private void Step7()
        {
            this._AddressBook.Save();
        }

        private void CreateAddressBookUseCase4()
        {
            Contact NewContact;
            Address NewAddress;

            NewContact = new Contact();
            NewContact.Name = "An Dematras";
            NewContact.PhoneNumber = "02/5820103";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "André Hazes";
            NewContact.EmailAddress = "andre@heaven.com";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "Jan Franchipan";
            NewContact.EmailAddress = "jan@eigenbelang.be";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "Josephine DePin";
            NewContact.EmailAddress = "jospin@proximus.be";
            NewContact.PhoneNumber = "054/44.87.26";
            NewAddress = new Address("Weverijstraat 12", "9500", "Geraardsbergen");
            NewContact.Address = NewAddress;
            _AddressBook.Add(NewContact);

            _AddressBook.Save();
        }
    }
}