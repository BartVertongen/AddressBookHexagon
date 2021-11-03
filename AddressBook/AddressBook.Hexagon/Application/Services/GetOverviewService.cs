// By Bart Vertongen copyright 2021

using System.Linq;
using System.Collections.Generic;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Adapters;


namespace PS.AddressBook.Hexagon.Application.Services
{
    public class GetOverviewService : IGetOverviewQuery
    {
        private readonly IAddressBookFile _LoadAddressBookPort;

        public GetOverviewService(IAddressBookFile file)
        {
            _LoadAddressBookPort = file;
        }

        public List<IContactLineDTO> GetOverview(string filter = "")
        {
            IAddressBookDTO oAddressBookDTO = new AddressBookDTO();
            IAddressBookDTO oWorkContacts = null;
            List<ContactDTO> tempContactList = null;
            List<IContactLineDTO> Result = new();
            int iID = 0;

            _LoadAddressBookPort.Load(ref oAddressBookDTO);
            //If the AddressBook is empty we do not need to filter.
            if (oAddressBookDTO.Count == 0) return Result;
            //Do the filtering here
            if (string.IsNullOrEmpty(filter))
                oWorkContacts = oAddressBookDTO;
            else
            {
                if (!string.IsNullOrEmpty(filter) && !filter.Contains("*"))
                {
                    tempContactList = oAddressBookDTO
                            .Where(ctt => ctt.Name.ToUpper().Contains(filter.ToUpper())).ToList();
                }
                else if (!string.IsNullOrEmpty(filter) && filter[0] == '*')
                {
                    tempContactList = oAddressBookDTO
                            .Where(ctt => ctt.Name.ToUpper().EndsWith(filter.ToUpper())).ToList();
                }
                else if (!string.IsNullOrEmpty(filter) && filter[filter.Length - 1] == '*')
                {
                    tempContactList = oAddressBookDTO
                            .Where(ctt => ctt.Name.ToUpper().StartsWith(filter.ToUpper())).ToList();
                }
                //We need to convert from List<ContactDTO> TO IAddressBookDTO
                if (tempContactList is not null)
                    oWorkContacts = new AddressBookDTOAdapter(tempContactList);
            }
            if (oWorkContacts is not null)
            {
                foreach (IContactDTO dtoContact in oWorkContacts)
                {
                    IContactLineDTO oContactLine;
                    ContactDTOMapper oContactDTOMapper = new();

                    oContactLine = new ContactLineDTO
                    {
                        Id = ++iID,
                        Name = dtoContact.Name,
                        ContentsCode = oContactDTOMapper.MapFrom(dtoContact).ContentsCode
                    };
                    Result.Add(oContactLine);
                }
            }
            return Result;
        }
    }
}