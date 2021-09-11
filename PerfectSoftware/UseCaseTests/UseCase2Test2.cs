//Copyright 2021 Bart Vertongen.

using System;
using Xunit;
using PS.AddressBook.Business;
using PS.AddressBook.Business.Commands;
using PS.AddressBook.Business.Interfaces;


namespace UseCaseTests2
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase2Test2 : IDisposable
    {
        private AddressBook _AddressBook;
        private Contact _Contact;
        private IConsole _Console;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase2Test2()
        {
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase2.xml";
            _Contact = new Contact(_AddressBook);
            _Console = new TestConsole();
        }

        /// <summary>
        /// The cleanup code.
        /// </summary>
        public void Dispose()
        {
            _AddressBook.Clear();
        }

        [Theory]
        [InlineData("Anthony Hopkins", "", "", "", "+3202530014", "AHopkins@stars.com")]
        [InlineData("Jan Franchipan", "Weverijstraat 12", "9500", "Geraardsbergen", "+3254/48.72.49", "janfranchi@telenet.be")]
        [InlineData("An Delmare", "", "", "", "", "an_weetal@proximus.be")]
        public void UseCase2Main_ValidData_ShouldBeSuccessful(string name, string street, string postalcode,
                                        string town, string phone, string email)
        {
            //Arrange
            IChangeCommandResponse aResponse;
            AddContactCommand AddAContactCommand;
            AddAContactCommand = NewMethod();

            //Action
            //Step1: USER trigger the adding a new Contact.
            // This is done by starting the Unit Test.

            //Step2 SYSTEM Asks for Input of the Name
            _Console.Write("Give in a Unique name for the new Contact: ");

            //Step3: USER supplies a Name
            //Step4: SYSTEM will validate the Name
            TestConsole.UserInput = name;
            _Contact.Name = _Console.ReadLine();
            _Console.WriteLine();

            //Step5: UseCase 2.5: Give in the New Address



            //Step6: The SYSTEM will ask for the Phone Number.
            _Console.Write("Give in a Phone number for the new Contact: ");

            //Step7: The USER Supplies a Valid Phone Number.
            TestConsole.UserInput = phone;
            _Contact.PhoneNumber = _Console.ReadLine();
            _Console.WriteLine();

            //Step8: The SYSTEM asks for a Valid Email Address.
            _Console.Write("Give in an Email Address for the new Contact: ");

            //Step9: The User Supplies a Valid Email Adress.
            TestConsole.UserInput = email;
            _Contact.Email = _Console.ReadLine();
            _Console.WriteLine();

            //Step10: The SYSTEM will add the new Contact to the AddressBook and Xml.          
            aResponse = AddAContactCommand.Run();

            //Assert
            Assert.True(aResponse.WasSuccessful);
           // Assert.True(_AddressBook.Count == 1);
        }


        private AddContactCommand NewMethod()
        {
            return new AddContactCommand(_AddressBook, _Contact);
        }


        /*/// <summary>
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
        }*/

    }
}