//By Bart Vertongen copyright 2021.


using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Commands;


namespace PS.AddressBook.Hexagon.Application.Mappers
{
    public class CreateContactCommandBuilderDTOMapper : IMapper<ICreateContactCommandBuilder, ICreateContactCommandBuilderDTO>
    {
        public ICreateContactCommandBuilder MapFrom(ICreateContactCommandBuilderDTO target)
        {
            ICreateContactCommandBuilder Result = new CreateContactCommandBuilder()
                .AddName(target.Name)
                .AddPhone(target.Phone)
                .AddEmail(target.Email)
                .AddStreet(target.Street)
                .AddPostalCode(target.PostalCode)
                .AddTown(target.Town);

            return Result;
        }

        public ICreateContactCommandBuilderDTO MapTo(ICreateContactCommandBuilder source)
        {
            /*ICreateContactCommandBuilder Result = new CreateContactCommandBuilderDTO()
            {

            }
            return Result;*/
            throw new System.NotImplementedException();
        }
    }
}