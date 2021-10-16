// By Bart Vertongen copyright 2021.

using System;


namespace PS.AddressBook.Hexagon.Domain.Ports
{
    /// <summary>
    /// The interface for an adress.
    /// </summary>
    public interface IAddress: IEquatable<IAddress>
    {
        string Street { get; set; }

        string PostalCode { get; set; }

        string Town { get; set; }

        /// <summary>
        /// Checks whether the address is a valid address.
        /// </summary>
        /// <returns></returns>
        bool IsValid();

        /// <summary>
        /// Creates a Deep Copy of this Address.
        /// </summary>
        /// <returns></returns>
        IAddress DeepClone();

        void Assign(IAddress newvalues);
    }
}