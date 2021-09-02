﻿//By Bart Vertongen copyright 2021

using System.Runtime.Serialization;
using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Data
{
    /// <summary>
    /// Address Data Transfer Type.
    /// </summary>
    [DataContract(Name = "Address", Namespace = "http://vertongensoftware.be")]
    public class AddressDTO : IAddressDTO
    {
        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string Town { get; set; }
    }
}