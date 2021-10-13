// By Bart Vertongen copyright 2021

using System.Collections.Generic;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace AddressBook.Hexagon.Application.Services
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
            List<IContactLineDTO> Result = new();

            _LoadAddressBookPort.Load(oAddressBookDTO);
            foreach(IContactDTO dtoContact in oAddressBookDTO)
            {
                IContactLineDTO dtoContactLine;

                AdapterFromContactDTO Adapter = new(dtoContact);
                dtoContactLine = Adapter.ContactLine;
                Result.Add(dtoContactLine);
            }
            return Result;
        }
    }
}