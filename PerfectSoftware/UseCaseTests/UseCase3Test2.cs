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
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase3Test2
    {
        private AddressBook _AddressBook;
        private Contact _Contact;
        private IConsole _Console;
        private IFile _File;
        private Mock<IFile> FileMock;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase3Test2()
        {
            //We should not use a real File but Mock it.
            FileMock = new Mock<IFile>();
            FileMock.Setup(f => f.Exists(It.IsAny<String>())).Returns(true);
            FileMock.Setup(f => f.Delete(It.IsAny<String>()));

            _File = FileMock.Object;
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase3.xml";
            if (_File.Exists(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile))
            {
                _File.Delete(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile);
            }
            this.CreateAddressBookUseCase3();
            _Console = new TestConsole();
        }

        /// <summary>
        /// UseCase3 Main
        /// </summary>
        [Theory]
        [InlineData("andr","xx", "xx", "xx", "0081780", "andre@hell.com")]
        [InlineData("*demat*", "xx", "xx", "xx", "", "an1989@telenet.be")]
        public void UseCase3_UpdateExistingContact_ShouldGiveChangedContact(string filter, string newStreet,
                    string newPostCode, string newTown, string newPhone, string newEmail)
        {
            //Arrange
            IContactLineDTO oContactLine;
            UpdateContactCommand UpdateCommand;
            IChangeCommandResponse UpdateResponse;
            IQueryCommandResponse oResponse;
            _AddressBook.Load();

            //Actions
            //Step1: The User requests a Contact Update
            //  This is done by starting this Unit Test.

            //Step2: The SYSTEM asks for a filter to show possible Contacts.
            //Step3: The User gives the filter pattern.
            GetOverViewCommand GetList = new GetOverViewCommand(_AddressBook, filter);

            //Step4: The System shows all possible Contacts.
            oResponse = GetList.Run();

            //Step5: The User selects the Contact he wants to Update.
            //Step6: The System Retrieves the Contact with that Name.
            oContactLine = oResponse.Result[oResponse.Result.Count - 1];
            _Contact = (Contact)_AddressBook.GetContact(oContactLine.Name);

            //Step7: Use Case3.7 to Update the Address.            
            //Case3.7 Step1: The System shows the old Street value.
            _Console.WriteLine($"The Current value for the Street is : {_Contact.Address.Street}");
            //Case3.7 Step2: The Systems asks for a Street Input.
            _Console.Write("Give in a Street for the new Address for the new Contact or xx to keep the old one: ");
            //Case3.7 Step3 The User supplies a new Street.
            TestConsole.UserInput = newStreet;
            string Input = _Console.ReadLine();
            if (Input.ToUpper() != "XX")  _Contact.Address.Street = Input;
            _Console.WriteLine();

            //Case3.7 Step4: The System shows the old Postal Code.
            _Console.WriteLine($"The Current value for the Postal Code is : {_Contact.Address.PostalCode}");
            //Case3.7 Step5: The System asks for the new value of the Postal Code.
            _Console.Write("Give in a PostalCode for the new Address for the new Contact or xx to keep the old one: ");
            //Case3.7 Step6: The User gives in the new valid value for the Postal Code.
            TestConsole.UserInput = newPostCode;
            Input = _Console.ReadLine();
            if (Input.ToUpper() != "XX") _Contact.Address.PostalCode = Input;
            _Console.WriteLine();
            //Case3.7 Step7: The System shows the old Town.
            _Console.WriteLine($"The Current value for the Town is : {_Contact.Address.Town}");
            //Case3.7 Step8: The System asks for the new value of the Town.
            _Console.Write("Give in a new Town for the Address for the Contact or xx to keep the old one: ");
            //Case3.7 Step9: The User gives in the new value for the Town.
            TestConsole.UserInput = newTown;
            Input = _Console.ReadLine();
            //Case3.7 Step10: The System sets the new value for the Town.
            if (Input.ToUpper() != "XX") _Contact.Address.Town = Input;
            _Console.WriteLine();

            //Step8: Use Case3.8 to Update The PhoneNumber
            //Use Case3.8: Step1: The System shows the old PhoneNumber.
            _Console.WriteLine($"The Current value for the Phone Number is : {_Contact.PhoneNumber}");
            //Use Case3.8: Step2: The Systems asks for a PhoneNumber Input.
            _Console.Write("Give in a new Phone Number for the Contact or xx to keep the old one: ");
            //Use Case3.8: Step3: The User supplies a Not Null PhoneNumber.
            TestConsole.UserInput = newPhone;
            Input = _Console.ReadLine();
            //Use Case3.8: Step4: The SYSTEM sets the PhoneNumber.
            if (Input.ToUpper() != "XX") _Contact.PhoneNumber = Input;
            _Console.WriteLine();

            //Step9: Use Case3.9 to update the EmailAddress
            //Use Case3.9: Step1: The System shows the old Email.
            _Console.WriteLine($"The Current value for the Email is : {_Contact.Email}");
            //Use Case3.9: Step2: The Systems asks for a Email Input.
            _Console.Write("Give in a new Email for the Contact or xx to keep the old one: ");
            //Use Case3.9: Step3: The User supplies a Not Null Email.
            TestConsole.UserInput = newEmail;
            Input = _Console.ReadLine();
            //Use Case3.9: Step4:The System sets the Email for the Current Contact.
            if (Input.ToUpper() != "XX") _Contact.Email = Input;
            _Console.WriteLine();

            //Step10: Make the changes persistent and update the AddressBook.
            UpdateCommand = new UpdateContactCommand(_AddressBook, _Contact);
            UpdateResponse = UpdateCommand.Run();

            //Assert
            Assert.True(UpdateResponse.WasSuccessful);
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