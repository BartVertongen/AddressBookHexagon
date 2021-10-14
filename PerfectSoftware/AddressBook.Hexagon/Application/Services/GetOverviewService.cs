// By Bart Vertongen copyright 2021

using System.Collections.Generic;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application;


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
            List<IContactDTO> oAddressBookDTO = new List<IContactDTO>();
            List<IContactLineDTO> Result = new();

            _LoadAddressBookPort.Load(oAddressBookDTO);
            foreach(IContactDTO dtoContact in oAddressBookDTO)
            {
                IContactLineDTO dtoContactLine;
                IContact oContact;

                oContact = new Contact
                {
                    Name = dtoContact.Name,
                    Phone = dtoContact.PhoneNumber,
                    Email = dtoContact.Email,
                    Address = new Address
                    {
                        Street = dtoContact.Address.Street,
                        PostalCode = dtoContact.Address.PostalCode,
                        Town = dtoContact.Address.Town
                    }
                };
                dtoContactLine = oContact.ContactLine;
                Result.Add(dtoContactLine);
            }
            return Result;
        }
    }
}