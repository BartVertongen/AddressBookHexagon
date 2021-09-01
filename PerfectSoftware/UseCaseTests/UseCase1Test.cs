//Copyright 2021 Bart Vertongen.

using System;
using System.IO;
using System.Collections.Generic;
using Xunit;
using PS.AddressBook.Business;


namespace UseCaseTests
{
    /// <summary>
    /// Give Overview of All Contacts with possible filtering
    /// </summary>
    public class UseCase1Test
    {
        private AddressBook _AddressBook;
        private List<ContactLine> _ResultList;
        private string _Filter = "";

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase1Test()
        {
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase1.xml";
            if (!File.Exists(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile))
                this.CreateAddressBookUseCase1();
            _Filter = "";           
        }

        /// <summary>
        /// UseCase1 extend 2A
        /// </summary>
        [Fact]
        public void UseCase1Extend2A_WithNoFilter_ShouldGiveAll()
        {
            //Arrange
            _AddressBook.Load();
                
            //Actions
            this.Step1("");
            this.Step2();
            this.Step3();

            //Assert
            Assert.True(_ResultList.Count == 4);
        }

        /// <summary>
        /// UseCase1 extend 2B
        /// </summary>
        [Fact]
        public void UseCase1Extend2B_StartWithFilterA_ShouldGive()
        {
            //Arrange
            _AddressBook.Load();

            //Action
            this.Step1("A");
            this.Step2();
            this.Step3();

            //Assert
            Assert.True(_ResultList.Count == 2);
            Assert.Equal("An Dematras", _ResultList[0].Name);
        }

        /// <summary>
        /// UseCase1 extend 2C
        /// </summary>
        [Fact]
        public void UseCase1Extend2C_StartWithFilterDE_ShouldGive()
        {
            //Arrange
            _AddressBook.Load();

            //Action
            this.Step1("*de*");
            this.Step2();
            this.Step3();

            //Assert
            Assert.True(_ResultList.Count == 2);
            Assert.Equal("Josephine DePin", _ResultList[1].Name);
        }

        private void Step1(string filter)
        {
            _Filter = filter;
        }

        /// <summary>
        /// User asks for an overview of the adress Book Contacts with No filtering.
        /// </summary>
        private void Step2()
        {          
            this._ResultList = _AddressBook.GetOverview(_Filter);
        }

        /// <summary>
        /// The System collects the Contacts passing the filter and shows them.
        /// </summary>
        private void Step3(string filter="")
        {
            if (this._ResultList.Count == 0)
            {
                Console.WriteLine("There are no Contact to show!");
            }
            else
            {
                string CurrentLetter, PreviousLetter="";

                foreach (ContactLine oContactLn in this._ResultList)
                {
                    CurrentLetter = oContactLn.Name.Substring(0, 1);
                    if (PreviousLetter != CurrentLetter)
                    {
                        //CR Should not use UI here:
                        //Console.WriteLine("Tab {CurrentLetter}");
                        PreviousLetter = CurrentLetter;
                    }
                    //CR Should not use UI here:
                    //Console.WriteLine($"{iCount})\t{oContactLn.Name}\t{sInfoCode}");
                }
            }
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