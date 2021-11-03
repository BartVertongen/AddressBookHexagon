//By Bart Vertongen copyright 2021.

using PS.AddressBook.Hexagon.Application.Ports;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace PS.AddressBook.Hexagon.Application
{
    /// <summary>
    /// AddressBook Data Transfer Object
    /// </summary>
    [XmlRoot(ElementName = "AddressBook")]
    public class AddressBookDTO : List<ContactDTO>, IAddressBookDTO
    {

    }
}