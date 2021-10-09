// By Bart Vertongen copyright 2021.

using System;


namespace PS.AddressBook.Hexagon.Domain.Core
{
    /// <summary>
    /// The interface for an adress.
    /// </summary>
    public interface IAddress: IEquatable<IAddress>
    {
        /// <summary>
        /// The Street of the Address. Also contains number and box.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// A Code related with the Town.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// The Town of the address.
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Checks whether the address is a valid address.
        /// </summary>
        /// <returns></returns>
        bool IsValid();

        public void Assign(IAddress newvalues);
    }
}