//By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using System.Xml.Serialization;


namespace PS.AddressBook.Hexagon.Domain
{
    /// <summary>
    /// AddressBook Data Transfer Object
    /// </summary>
    [XmlRoot(ElementName = "AddressBook")]
    public class AddressBookDTO : List<IContactDTO>, IAddressBookDTO 
    { }
}