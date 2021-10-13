
using System;


namespace PS.AddressBook.Hexagon.Application.Messaging
{
	public interface ICommand : IMessage
    {
		/// <summary>
		/// Gets the command identifier.
		/// </summary>
		Guid Id { get; }
	}
}