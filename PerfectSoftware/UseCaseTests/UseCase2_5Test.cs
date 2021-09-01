//Copyright 2021 Bart Vertongen.

using Xunit;
using PS.AddressBook.Business;


namespace UseCaseTests
{
    /// <summary>
    /// Creation of a new Contact in AddressBook.
    /// </summary>
    public class UseCase2_5Test
    {
        public Address  Address;
        private string  _TempStreet;
        private string  _TempPostCode;
        private string  _TempTown;

        /// <summary>
        /// UseCase2 Main
        /// </summary>
        /// <remarks>
        /// Starting this test is equal to trigger for Contact creation.
        /// So this is Step 1.
        /// </remarks>
        [Fact]
        public void UseCase2_5_CreationValidAdress_GivesFullAddress()
        {
            //Arrange
                
            //Actions
            this.Step1And2("Avenue Louise 101");
            this.Step3And4("1000");
            this.Step5And6("Brussels");
            this.Step7();
            
            //Assert
            Assert.NotNull(this.Address);
        }

        /// <summary>
        /// The Systems asks for a Street Input.
        /// The User supplies a Not Null Street.
        /// </summary>
        /// <param name="newStreet"></param>
        private void Step1And2(string newStreet)
        {
            this._TempStreet = newStreet;
        }

        /// <summary>
        /// The Systems asks for a PostalCode Input.
        /// The User supplies a Not Null PostalCode.
        /// </summary>
        private void Step3And4(string newPostalCode)
        {
            this._TempPostCode = newPostalCode;
        }

        /// <summary>
        /// UseCase2.5 Give in the New Valid Address
        /// </summary>
        /// <param name="street"></param>
        /// <param name="zipcode"></param>
        /// <param name="town"></param>
        private void Step5And6(string town)
        {
            this._TempTown = town;
        }

        /// <summary>
        /// The System Creates A Valid Address with the Input.
        /// </summary>
        private void Step7()
        {
            this.Address = new Address(_TempStreet, _TempPostCode, _TempTown);
        }
    }
}