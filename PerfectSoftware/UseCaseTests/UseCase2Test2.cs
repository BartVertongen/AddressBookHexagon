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
        [InlineData("David Deschepper", "", "", "", "+3209/45.14.81", "")]
        public void UseCase2Main_ValidData_ShouldBeSuccessful(string name, string street, 
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
            Assert.True(aResponse.WasSuccessful);
        }


        [Theory]
        [InlineData("Anthony Hopkins", "", "", "", "+3202530014", "AHopkins@stars.com")]
        [InlineData("Jan Franchipan", "Weverijstraat 12", "9500", "Geraardsbergen", "+3254/48.72.49", "janfranchi@telenet.be")]
        [InlineData("An Delmare", "", "", "", "", "an_weetal@proximus.be")]
        [InlineData("David Deschepper", "", "", "", "+3209/45.14.81", "")]
        public void UseCase2Main_ValidData_ShouldHigherContactCount(string name, string street, string postalcode,
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
            Assert.True(_AddressBook.Count == 1);
        }

        private AddContactCommand NewMethod()
        {
            return new AddContactCommand(_AddressBook, _Contact);
        }
    }
}