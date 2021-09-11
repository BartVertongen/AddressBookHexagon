//Copyright 2021 Bart Vertongen.

using System;
using System.IO.Abstractions;
using Xunit;
using Moq;
using PS.AddressBook.Business;
using PS.AddressBook.Business.Commands;
using PS.AddressBook.Business.Interfaces;


namespace UseCaseTests2
{
    /// <summary>
    /// The Use Case to Delete a Contact.
    /// </summary>
    /// <remarks>
    /// The first step is the trigger: the user initiates the delete.
    /// In fact this is done by starting the UseCase4Test.
    /// </remarks>
    public class UseCase4Test2
    {
        private AddressBook _AddressBook;
        private IFile _File;
        private Mock<IFile> FileMock;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase4Test2()
        {
            //We should not use a real File but Mock it.
            FileMock = new Mock<IFile>();
            FileMock.Setup(f => f.Exists(It.IsAny<String>())).Returns(true);
            FileMock.Setup(f => f.Delete(It.IsAny<String>()));

            _File = FileMock.Object;
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase4.xml";
            if (_File.Exists(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile))
            {
                _File.Delete(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile);
            }
            this.CreateAddressBookUseCase4();         
        }

        [Theory]
        [InlineData("jan", 1)]
        [InlineData("*de*", 1)]
        public void UseCase4(string filter, int selectedContact)
        {
            //Arrange
            IContactLineDTO oContactLine;
            _AddressBook.Load();

            //ACTIONS
            //Step1
            GetOverViewCommand GetList = new GetOverViewCommand(_AddressBook, filter);
            DeleteContactCommand DeleteCommand;
            IChangeCommandResponse DeleteResponse;

            //Step 2
            IQueryCommandResponse oResponse;
            oResponse = GetList.Run();

            //Step 3: the Result for the list
            //Assert
            Assert.True(oResponse.WasSuccessful);
            Assert.True(oResponse.Result.Count > 0);

            //Step 4 is not done, we show nothing

            //Step 5: The USER selects the Contact he wants to delete.
            // There is no USER so we take the first
            oContactLine = oResponse.Result[selectedContact-1];

            //Step6 and step7: both are done by the SYSTEM and can be together.
            //Step6: Delete the Contact from Memory
            //Step7: Make the Delete persistent.
            DeleteCommand = new DeleteContactCommand(_AddressBook, oContactLine.Name);
            DeleteResponse = DeleteCommand.Run();

            //Asserts
            Assert.True(DeleteResponse.WasSuccessful);
            Assert.False(_AddressBook.ContainsName(oContactLine.Name));
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