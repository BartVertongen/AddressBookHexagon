// By Bart Vertongen.

using System.Collections.Generic;


namespace PS.AddressBook.Hexagon.Application.Ports
{
    //We can not Serialize IList<IContactDTO> but we can with IList<ContactDTO>
    public interface IAddressBookDTO: IList<ContactDTO>
    {
    }
}