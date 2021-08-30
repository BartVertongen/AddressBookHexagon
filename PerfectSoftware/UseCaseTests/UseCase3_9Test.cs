//Copyright 2021 Bart Vertongen.

using Xunit;
using AddressBookLib;


namespace UseCaseTests
{
    /// <summary>
    /// Update of Email in the Contact.
    /// </summary>
    public class UseCase3_9Test
    {
        public Contact  Contact;
        private string  _TempEmail;

        public UseCase3_9Test()
        {
            Contact = new Contact();
        }

        /// <summary>
        /// UseCase3_9 Main
        /// </summary>
        /// <remarks>
        /// Starting this test is equal to trigger for Email update.
        /// So this is Step 1.
        /// </remarks>
        [Fact]
        public void UseCase3_9_UpdateWith_ValidEmail()
        {
            //Arrange
                
            //Actions
            this.Step1_2And3("josefine@telenet.be");
            this.Step4();

            //Assert
            Assert.Equal("josefine@telenet.be", Contact.Email);
        }

        /// <summary>
        /// The System shows the old Email.
        /// The Systems asks for a Email Input.
        /// The User supplies a Not Null Email.
        /// </summary>
        /// <param name="newEmail"></param>
        private void Step1_2And3(string newEmail)
        {
            this._TempEmail = newEmail;
        }

        /// <summary>
        /// END: The System sets the Email for the Current Contact.
        /// </summary>
        private void Step4()
        {
            this.Contact.Email = this._TempEmail;
        }
    }
}