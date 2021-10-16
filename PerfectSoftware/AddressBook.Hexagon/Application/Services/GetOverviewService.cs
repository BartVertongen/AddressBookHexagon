// By Bart Vertongen copyright 2021

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
            List<IContactLineDTO> Result = new();
            int iID = 0;

            _LoadAddressBookPort.Load(oAddressBookDTO);
            foreach(IContactDTO dtoContact in oAddressBookDTO)
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