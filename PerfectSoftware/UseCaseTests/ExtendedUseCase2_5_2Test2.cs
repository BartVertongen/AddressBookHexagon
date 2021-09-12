//Copyright 2021 Bart Vertongen.

using Xunit;
using PS.AddressBook.Business;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.Business.Commands;


namespace UseCaseTests2
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase2_5_2Test2
    {
        private AddressBook _AddressBook;
        private Contact _Contact;
        private IConsole _Console;

        /// <summary>
        /// All the initialization for the tests.
        /// </summary>
        public UseCase2_5_2Test2()
        {
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase2.xml";
            _Contact = new Contact(_AddressBook);
            _Console = new TestConsole();
        }

        /// <summary>
        /// UseCase2 Main
        /// </summary>
        /// <remarks>
        /// Starting this test is equal to trigger for Contact creation.
        /// So this is Step 1.
        /// </remarks>
        [Theory]
        [InlineData("Jan Franchipan", "", "9500", "Geraardsbergen", "+3254/48.72.49", "janfranchi@telenet.be")]
        [InlineData("Jan Franchipan", "Weverijstraat 12", "", "Geraardsbergen", "+3254/48.72.49", "janfranchi@telenet.be")]
        [InlineData("Jan Franchipan", "Weverijstraat 12", "9500", "", "+3254/48.72.49", "janfranchi@telenet.be")]
        public void UseCase2_5_2_CreationAdressWithMissingData_ShouldGiveEmptyAddress(string name, string street, 
                                                            string postalcode, string town, string phone, string email)
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
            //UseCase2.5: Step1: The SYSTEM asks for the Street.
            _Console.Write("Give in a Street for the new Address for the new Contact: ");

            //UseCase2.5 Step2: The User gives a valid Street.
            TestConsole.UserInput = street;
            _Contact.Address.Street = _Console.ReadLine();
            _Console.WriteLine();

            //UseCase2.5 Step3: SYSTEM asks for the Postal Code.
            _Console.Write("Give in a Postal Code for the new Address for the new Contact: ");

            //UseCase2.5 Step4: The USER gives in a valid Postal Code.
            TestConsole.UserInput = postalcode;
            _Contact.Address.PostalCode = _Console.ReadLine();
            _Console.WriteLine();

            //UseCase2.5 Step5: SYSTEM asks for the Town.
            _Console.Write("Give in a Town for the new Address for the new Contact: ");

            //UseCase2.5: Step6: The USER gives in a valid Town.
            TestConsole.UserInput = town;
            _Contact.Address.Town = _Console.ReadLine();
            _Console.WriteLine();


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
            Assert.False(aResponse.WasSuccessful);
        }

        private AddContactCommand NewMethod()
        {
            return new AddContactCommand(_AddressBook, _Contact);
        }
    }
}