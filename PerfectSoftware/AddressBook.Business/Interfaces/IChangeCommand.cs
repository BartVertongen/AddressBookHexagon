﻿// By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Business.Interfaces
{
    public interface IChangeCommand: ICommand
    {

        IChangeCommandResponse Run();
    }
}