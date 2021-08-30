//Copyright 2021 Bart Vertongen.

using System;
using System.IO;
using Xunit;
using AddressBookLib;


namespace UseCaseTests
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class ExtendedUseCase2_4ATest
    {
        private AddressBook _AddressBook;
        private Contact _Contact;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public ExtendedUseCase2_4ATest()
        {
            Contact FirstContact;

            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase2_4A.xml";
            FirstContact = new Contact();
            FirstContact.Name = "Anthony Hopkins";
            FirstContact.PhoneNumber = "+3202530014";
            FirstContact.Email = "AHopkins@stars.com";
            _AddressBook.Add(FirstContact);
            _AddressBook.Save();
        }


        /// <summary>
        /// Extended UseCase2_4A: The Name given by the User for New Contact exists already
        /// </summary>
        /// <remarks>
        /// Starting this test is equal to trigger for Contact creation.
        /// So this is Step 1.
        /// </remarks>
        [Fact]
        public void UseCase2_4A_CreationWithExistingName_ShouldFail()
        {
            //Arrange

            //Actions
            Action testCode = () => this.Step2And3("Anthony Hopkins");
            var ex = Record.Exception(testCode);

            //Assert
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
        }

        /// <summary>
        /// Extended UseCase2_4A: The Name given by the User for New Contact does not yet exist.
        /// </summary>
        /// <remarks>
        /// Starting this test is equal to trigger for Contact creation.
        /// So this is Step 1.
        /// </remarks>
        [Fact]
        public void UseCase2_4A_CreationWithNOTExistingName_ShouldSucceed()
        {
            //Arrange

            //Actions
            Action testCode = () => this.Step2And3("Celine Hopkins");
            var ex = Record.Exception(testCode);

            //Assert
            Assert.Null(ex);
            Assert.Equal("Celine Hopkins", _Contact.Name);
        }

        /// <summary>
        /// The systems asks for a Name Input.
        /// The User supplies a Not Null Name.
        /// </summary>
        /// <param name="newName"></param>
        private void Step2And3(string newName)
        {
            _Contact = new Contact();
            _Contact.AddressBook = _AddressBook;
            _Contact.Name = newName;
        }
    }
}