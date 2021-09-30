

using System.Collections.Generic;
using System.Xml.Serialization;


namespace PS.AddressBook.Data
{
    /// <summary>
    /// AddressBook Data Transfer Object
    /// </summary>
    [XmlRoot(ElementName="AddressBook")]
    public class AddressBookDTO: List<ContactDTO>
    {

    }
}