

namespace PS.AddressBook.Hexagon.Application.Ports
{
    /// <remarks>
    /// We need both IUpdateContactCommandBuilderDTO and IUpdateContactCommandDTO to make a difference even though they are the same.
    /// This to force we only have a IUpdateContactCommandDTO after the Build method of UpdateContactCommandBuilder.
    /// </remarks>
    public interface IUpdateContactCommandBuilderDTO : IUpdateContactCommandDTO
    { }
}