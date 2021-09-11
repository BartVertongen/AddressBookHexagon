//Copyright 2021 Bart Vertongen.

using System;
using System.IO;
using Xunit;
using PS.AddressBook.Business;
using PS.AddressBook.Business.Commands;
using PS.AddressBook.Business.Interfaces;


namespace UseCaseTests2
{
    /// <summary>
    /// Give Overview of All Contacts with possible filtering
    /// </summary>
    public class UseCase1Test2
    {
        private AddressBook _AddressBook;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase1Test2()
        {
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase1.xml";
            if (File.Exists(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile))
            {
                File.Delete(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile);
            }
            this.CreateAddressBookUseCase1();        
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("*de*")]
        public void UseCase1(string filter)
        {
            //Arrange
            _AddressBook.Load();

            //Actions
            //Step1
            GetOverViewCommand GetList = new GetOverViewCommand(_AddressBook, filter);

            //Step2
            IQueryCommandResponse oResponse;
            oResponse = GetList.Run();

            //Step3: the Result
            //Assert
            Assert.True(oResponse.WasSuccessful);
            if (oResponse.WasSuccessful)
            {
                if (filter == "")
                    Assert.True(oResponse.Result.Count == 4);
                else if (filter == "a")
                {
                    Assert.True(oResponse.Result.Count == 2);
                    Assert.Equal("An Dematras", oResponse.Result[0].Name);
                }
                else if (filter == "*de*")
                {
                    Assert.True(oResponse.Result.Count == 2);
                    Assert.Equal("Josephine DePin", oResponse.Result[1].Name);
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