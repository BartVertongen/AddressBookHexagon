

namespace PS.AddressBook.Hexagon.Application.Ports
{
    /// <remarks>
    /// We need both ICreateContactCommandBuilderDTO and ICreateContactCommandDTO to make a differnce even though they are the same.
    /// This to force we only have a ICreateContactCommandDTO after the Build method of CreateContactCommandBuilder.
    /// </remarks>
    public interface ICreateContactCommandBuilderDTO : ICreateContactCommandDTO
    { }
}