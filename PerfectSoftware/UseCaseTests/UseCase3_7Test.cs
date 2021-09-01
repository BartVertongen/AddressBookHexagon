//Copyright 2021 Bart Vertongen.

using Xunit;
using PS.AddressBook.Business;


namespace UseCaseTests
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase3_7Test
    {
        public Address  Address;
        private string  _TempStreet;
        private string  _TempPostCode;
        private string  _TempTown;

        public UseCase3_7Test()
        {
            //_Address = address;
        }

        /// <summary>
        /// UseCase3_7 Main
        /// </summary>
        /// <remarks>
        /// Starting this test is equal to trigger for Contact creation.
        /// So this is Step 1.
        /// </remarks>
        [Fact]
        public void UseCase3_7_CreationValidAdress_GivesFullAddress()
        {
            //Arrange
                
            //Actions
            this.Step1_2And3("Avenue Louise 101");
            this.Step4_5And6("1000");
            this.Step7_8And9("Brussels");
            this.Step10();
            
            //Assert
            Assert.NotNull(Address);
            Assert.Equal("1000", Address.PostalCode);
        }

        /// <summary>
        /// The System shows the old Street value.
        /// The Systems asks for a Street Input.
        /// The User supplies a Not Null Street.
        /// </summary>
        /// <param name="newStreet"></param>
        private void Step1_2And3(string newStreet)
        {
            this._TempStreet = newStreet;
        }

        /// <summary>
        /// The System shows the old Postal Code.
        /// The System asks for the new value of the Postal Code.
        /// The User gives in the new valid value for the Postal Code.
        /// </summary>
        private void Step4_5And6(string newPostalCode)
        {
            this._TempPostCode = newPostalCode;
        }

        /// <summary>
        /// The System shows the old Town.
        /// The System asks for the new value of the Town.
        /// The User gives in the new value for the Town.
        /// </summary>
        /// <param name="town"></param>
        private void Step7_8And9(string town)
        {
            this._TempTown = town;
        }

        /// <summary>
        /// The System Creates A Valid Address with the Input.
        /// </summary>
        private void Step10()
        {
            this.Address = new Address(_TempStreet, _TempPostCode, _TempTown);
        }
    }
}