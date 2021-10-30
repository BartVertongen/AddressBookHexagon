﻿// By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application.UseCases
{
    /// <summary>
    /// Gives an Overview of the Contacts in the AddressBook
    /// </summary>
    /// <remarks>This is a Query not a Command.</remarks>
    public interface IGetContactWithNameQuery
    {
        IContactDTO GetContactWithName(string name);
    }
}