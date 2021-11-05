

namespace PS.AddressBook.Hexagon.Application.Ports
{
    /// <remarks>
    /// We need both IDeleteContactCommandBuilderDTO and IDeleteContactCommandDTO to make a difference even though they are the same.
    /// This to force we only have a IDeleteContactCommandDTO after the Build method of DeleteContactCommandBuilder.
    /// </remarks>
    public interface IDeleteContactCommandBuilderDTO : IDeleteContactCommandDTO
    { }
}