// By Bart Vertongen copyright 2021

using System.Linq;
using System.Collections.Generic;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.Mappers;


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
            List<IContactDTO> oAddressBookDTO = new();
            List<IContactDTO> oWorkContacts;
            List<IContactLineDTO> Result = new();
            int iID = 0;

            _LoadAddressBookPort.Load(oAddressBookDTO);
            //Do the filtering here
            if (!filter.Contains("*"))
                oWorkContacts = oAddressBookDTO.Where(ctt => ctt.Name.ToUpper().Contains(filter.ToUpper())).ToList();
            else if (filter[0] == '*')
                oWorkContacts = oAddressBookDTO.Where(ctt => ctt.Name.ToUpper().EndsWith(filter.ToUpper())).ToList();
            else if (filter[filter.Length - 1] == '*')
                oWorkContacts = oAddressBookDTO.Where(ctt => ctt.Name.ToUpper().StartsWith(filter.ToUpper())).ToList();
            else
                oWorkContacts = new();
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
            return Result;
        }
    }
}