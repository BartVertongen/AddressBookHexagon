//Copyright 2021 Bart Vertongen.

using Xunit;
using PS.AddressBook.Business;


namespace UseCaseTests
{
    /// <summary>
    /// Update of PhoneNumber in the Contact.
    /// </summary>
    public class UseCase3_8Test
    {
        public Contact  Contact;
        private string  _TempPhoneNumber;

        public UseCase3_8Test()
        {
            Contact = new Contact();
        }

        /// <summary>
        /// UseCase3_8 Main
        /// </summary>
        /// <remarks>
        /// Starting this test is equal to trigger for PhoneNumber update.
        /// So this is Step 1.
        /// </remarks>
        [Fact]
        public void UseCase3_8_UpdateWith_ValidPhoneNumber()
        {
            //Arrange
                
            //Actions
            this.Step1_2And3("09/45.77.48");
            this.Step4();

            //Assert
            Assert.Equal("09/45.77.48", Contact.PhoneNumber);
        }

        /// <summary>
        /// The System shows the old PhoneNumber.
        /// The Systems asks for a PhoneNumber Input.
        /// The User supplies a Not Null PhoneNumber.
        /// </summary>
        /// <param name="newPhone"></param>
        private void Step1_2And3(string newPhone)
        {
            this._TempPhoneNumber = newPhone;
        }

        /// <summary>
        /// END: The System sets the PhoneNumber for the Current Contact.
        /// </summary>
        private void Step4()
        {
            this.Contact.PhoneNumber = this._TempPhoneNumber;
        }
    }
}