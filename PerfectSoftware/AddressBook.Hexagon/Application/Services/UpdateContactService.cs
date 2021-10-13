

using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace PS.AddressBook.Hexagon.Application.Services
{
    /// <summary>
    /// This is an adapter for the DeleteConactUseCase.
    /// </summary>
    public class UpdateContactService : IUpdateContactUseCase
    {
        public void UpdateContact(UpdateContactCommand command)
        {
            // TODO: validate business rules
            // TODO: manipulate model state
            // TODO: return output
            throw new System.NotImplementedException();
        }
    }
}