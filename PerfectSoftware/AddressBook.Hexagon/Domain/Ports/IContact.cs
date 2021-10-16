// By Bart Vertongen copyright 2021

using System;


namespace PS.AddressBook.Hexagon.Domain.Ports
{
    /// <summary>
    /// The Interface for the Contact Business Object.
    /// </summary>
    public interface IContact : IEquatable<IContact>
    {
        public IAddressBook AddressBook { set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public IAddress Address { get; set; }


        /// <summary>
        /// Gives a Code to show what data is available in this Contact.
        /// </summary>
        public string ContentsCode { get; }

        /// <summary>
        /// The Address of the Contact
        /// </summary>
        /// <remarks>Hides the IAddressDTO type which is replaced by IAddress</remarks>
        public IAddress GetAddress();

        public void SetAddress(IAddress value);

        /// <summary>
        /// Flag whether this Contact contains valid data.
        /// </summary>
        /// <returns></returns>
        public bool IsValid();

        /// <summary>
        /// Creates a Deep Copy of this Contact.
        /// </summary>
        /// <returns></returns>
        public IContact DeepClone();

        public void Assign(IContact newvalues);
    }
}