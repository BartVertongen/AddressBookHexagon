//Copyright 2021 Bart Vertongen

using System;
using System.IO;
using PS.AddressBook.Hexagon.Domain.Ports;


namespace PS.AddressBook.Hexagon.Domain
{
    public class Contact : IContact, IEquatable<IContact>, IComparable<IContact>
    {
        private string _Name;
        private Address _Address;

        public Contact()
        {
            this.Name = "";
            this._Address = new Address();
            this.Phone = "";
            this.Email = "";
        }

        public Contact(IContact bussRef)
        {
            this.Name = bussRef.Name;
            this._Address = (Address)bussRef.Address.DeepClone();
            this.Phone = bussRef.Phone;
            this.Email = bussRef.Email;
        }

        /// <summary>
        /// Constructor for Contact.
        /// </summary>
        /// <param name="addressBook"></param>
        public Contact(IAddressBook addressBook)
        {
            AddressBook = addressBook;
            this._Address = new Address();
            this.Phone = "";
            this.Email = "";
        }

        /// <summary>
        /// The AddressBook to which this Contact belongs.
        /// </summary>
        public IAddressBook AddressBook { private get;  set;  }

        public string Name
        { 
            get { return _Name; }

            set
            {
                if (AddressBook != null && AddressBook.ContainsName(value))
                    throw new InvalidDataException($"Name {value} exists allready");
                else
                    _Name = value;
            } 
        }

        public IAddress Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        static public string GetContentsCode(IContact contact)
        {
            string sContentsCode;
            Address bussAddress = new()
            {
                Street = contact.Address.Street,
                PostalCode = contact.Address.PostalCode,
                Town = contact.Address.Town
            };
            sContentsCode = bussAddress.IsEmpty() ? "*" : "A";
            sContentsCode += string.IsNullOrEmpty(contact.Phone) ? "*" : "P";
            sContentsCode += string.IsNullOrEmpty(contact.Email) ? "*" : "E";
            return sContentsCode;
        }

        public string ContentsCode
        {
            get
            {
                return Contact.GetContentsCode(this);
            } 
        }

        static public bool IsValid (IContact contact)
        {
            if (string.IsNullOrEmpty(contact.Name))
                return false;
            else if (string.IsNullOrEmpty(contact.Phone) && string.IsNullOrEmpty(contact.Email))
                return false;
            else if (!contact.Address.IsValid())
                return false;
            else
                return true;
        }

        /// <summary>
        /// Checks whether the Contact is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return Contact.IsValid(this);
        }

        /// <summary>
        /// Creates a Deep Copy of this Contact.
        /// </summary>
        /// <returns></returns>
        public IContact DeepClone()
        {
            Contact oCopy = new()
            {
                Name = this.Name,
                Phone = this.Phone,
                Email = this.Email,
                _Address = new Address(_Address.Street, _Address.PostalCode, _Address.Town)
            };
            return oCopy;
        }

        /// <summary>
        /// Determins whether a Contact is equal or not.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if equal</returns>
        /// <remarks>This is needed to be able to remove a Contact from a list.</remarks>
        public bool Equals(IContact other)
        {
            if (this.Name != other.Name)
                return false;
            else if (this.Phone != other.Phone)
                return false;
            else if (this.Email != other.Email)
                return false;
            else if (this._Address.Equals(other.Address))
                return true;
            else
                return false;
        }

        public void Assign(IContact newValues)
        {
            this.Phone = newValues.Phone;
            this.Email = newValues.Email;
            this._Address.Assign(newValues.Address);
        }

        public int CompareTo(IContact other)
        {
            return String.Compare(this.Name, other.Name);
        }
    }
}