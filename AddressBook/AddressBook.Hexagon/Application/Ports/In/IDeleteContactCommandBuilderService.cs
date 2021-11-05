//By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Hexagon.Application.Ports
{
    public interface IDeleteContactCommandBuilderService
    {
        IDeleteContactCommandBuilderDTO AddName(string name, IDeleteContactCommandBuilderDTO deleteCommandDTO);

        IDeleteContactCommandDTO Build(string email, IDeleteContactCommandBuilderDTO deleteCommandBuilder);
    }
}