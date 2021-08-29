//Copyright 2021 Bart Vertongen.

using Xunit;
using AddressBookLib;


namespace UseCaseTests
{
    /// <summary>
    /// Creation of a new Address in Contact.
    /// </summary>
    public class UseCase2_5_2CTest
    {
        public Address  Address;
        private string  _TempStreet;
        private string  _TempPostCode;
        private string  _TempTown;

        /// <summary>
        /// UseCase2 Main
        /// </summary>
        /// <remarks>
        /// Starting this test is equal to trigger for Address creation.
        /// So this is Step 1.
        /// </remarks>
        [Fact]
        public void UseCase2_5_2B_CreationAdressWithEmptyTown_GivesEmptyAddress()
        {
            //Arrange
                
            //Actions
            this.Step1And2("Nieuwstraat");
            this.Step3And4("1000");
            this.Step5And6("");
            this.Step7();
            
            //Assert
            Assert.NotNull(this.Address);
            Assert.Equal("", this.Address.Street);
            Assert.Equal("", this.Address.PostalCode);
            Assert.Equal("", this.Address.Town);
        }

        /// <summary>
        /// The Systems asks for a Street Input.
        /// The User supplies a valid Street.
        /// </summary>
        /// <param name="newStreet"></param>
        private void Step1And2(string newStreet)
        {
            this._TempStreet = newStreet;
        }

        /// <summary>
        /// The Systems asks for a PostalCode Input.
        /// The User supplies a valid PostalCode.
        /// </summary>
        private void Step3And4(string newPostalCode)
        {
            this._TempPostCode = newPostalCode;
        }

        /// <summary>
        /// The Systems asks for a Town Input.
        /// The User supplies an empty Town.
        /// </summary>
        /// <param name="town"></param>
        private void Step5And6(string town)
        {
            this._TempTown = town;
        }

        /// <summary>
        /// The System Creates an Address with the given Input.
        /// </summary>
        private void Step7()
        {
            this.Address = new Address(_TempStreet, _TempPostCode, _TempTown);
        }
    }
}