//Copyright 2021 Bart Vertongen.

using System;
using System.IO;
using Xunit;
using PS.AddressBook.Business;


namespace UseCaseTests
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase2Test
    {
        private AddressBook _AddressBook;
        private Contact _Contact;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase2Test()
        {
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase2.xml";
            _Contact = new Contact(_AddressBook);           
        }

        /// <summary>
        /// UseCase2 Main
        /// </summary>
        /// <remarks>
        /// Starting this test is equal to trigger for Contact creation.
        /// So this is Step 1.
        /// </remarks>
        [Fact]
        public void UseCase2_CreationValidContact_ShouldGiveFullContentsContact()
        {
            //Arrange
                
            //Actions
            this.Step2And3("Anthony Hopkins");
            this.Step4();
            this.Step5();
            this.Step6And7("+3202530014");
            this.Step8And9("AHopkins@stars.com");
            this.Step10();
            _AddressBook.Save();
            _AddressBook.Load();

            //Assert
            Assert.True(_AddressBook.Count == 1);
        }


        /// <summary>
        /// The systems asks for a Name Input.
        /// The User supplies a Not Null Name.
        /// </summary>
        /// <param name="newName"></param>
        private void Step2And3(string newName)
        {
            _Contact.Name = newName;
        }

        /// <summary>
        /// The System checks if the Name is new and valid.
        /// </summary>
        private void Step4()
        {
            if (string.IsNullOrEmpty(_Contact.Name))
                throw new InvalidDataException("The ContactName is empty!");
            if (_AddressBook.ContainsName(_Contact.Name))
                throw new InvalidDataException("The ContactName already exists!");
        }

        /// <summary>
        /// UseCase2.5 Give in the New Valid Address
        /// </summary>
        /// <param name="street"></param>
        /// <param name="zipcode"></param>
        /// <param name="town"></param>
        private void Step5()
        {
            UseCase2_5Test ACase2_5Test = new UseCase2_5Test();
            ACase2_5Test.UseCase2_5_CreationValidAdress_GivesFullAddress();
            _Contact.Address = ACase2_5Test.Address;
        }

        /// <summary>
        /// The System will ask for the Phone Number.
        /// The User Supplies a Valid Phone Number.
        /// </summary>
        private void Step6And7(string phone)
        {
            _Contact.PhoneNumber = phone;
        }

        /// <summary>
        /// The System asks for a Valid EmailAdress.
        /// The User Supplies a Valid EmailAdress.
        /// </summary>
        /// <param name="email"></param>
        private void Step8And9(string email)
        {
            _Contact.Email = email;
        }

        /// <summary>
        /// The System will add the new Contact to the AdressBook and Xml.
        /// </summary>
        private void Step10()
        {
            //Add to addressBook in memory
            _AddressBook.Add(_Contact);
            //Add th Xml AddressBook
            _AddressBook.Save();
        }
    }
}