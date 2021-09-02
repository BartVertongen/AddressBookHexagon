

using System.Collections.Generic;
using System.Xml.Serialization;


namespace PS.AddressBook.Data
{
    [XmlRoot(ElementName="AddressBook")]
    public class AddressBookDTO: List<ContactDTO>
    {

    }
}