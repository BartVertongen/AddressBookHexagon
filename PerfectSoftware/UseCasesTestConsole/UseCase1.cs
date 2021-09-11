
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PS.AddressBook.Business;


namespace UseCasesTestConsole
{
    /// <summary>
    /// Give Overview of All Contacts with possible filtering.
    /// </summary>
    public class UseCase1
    {
        private AddressBook _AddressBook;
        private List<ContactLineDTO> _ResultList;
        private string _Filter;

        public UseCase1()
        {
            this.PreCondition();
        }

        public void Execute()
        {
            this.Step1();
            this.Step2();
            this.Step3();
        }

        /// <summary>
        /// You need an AddressBook with Contacts.
        /// </summary>
        public void PreCondition()
        {
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase1.xml";
            if (File.Exists(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile))
            {
                File.Delete(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile);
            }
            this.CreateAddressBookUseCase1();
            _Filter = "";
        }

        /// <summary>
        /// START:TRIGGER: USER asks for an overview of the adress Book Contacts with possible filtering.
        /// </summary>
        public void Step1()
        {
            Console.WriteLine($"UseCase1: Give Overview of All Contacts with possible filtering.");
            Console.Write($"Give in the filter you want to Use: ");
            _Filter = Console.ReadLine();
            Console.WriteLine();
        }

        /// <summary>
        /// The SYSTEM gets all Contacts from the DB passing the filter.
        /// </summary>
        public void Step2()
        {
            this._ResultList = _AddressBook.GetOverview(_Filter).Cast<ContactLineDTO>().ToList();
        }

        /// <summary>
        /// END: The SYSTEM shows the collected Contacts.
        /// POSTCONDITION: We should have a list of the Contacts filtered by the given filter string.
        /// </summary>
        public void Step3()
        {
            if (this._ResultList.Count == 0)
            {
                Console.WriteLine("There are no Contact to show!");
            }
            else
            {
                string CurrentLetter, PreviousLetter = "";

                foreach (ContactLineDTO oContactLn in this._ResultList)
                {
                    CurrentLetter = oContactLn.Name.Substring(0, 1);
                    if (PreviousLetter != CurrentLetter)
                    {
                        Console.WriteLine($"Tab [{CurrentLetter}]");
                        PreviousLetter = CurrentLetter;
                    }
                    Console.WriteLine($"\t{oContactLn.Id}) {oContactLn.Name, -60} {oContactLn.ContentsCode}");
                }
            }
        }

        private void CreateAddressBookUseCase1()
        {
            Contact NewContact;
            Address NewAddress;

            NewContact = new Contact();
            NewContact.Name = "An Dematras";
            NewContact.PhoneNumber = "02/5820103";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "André Hazes";
            NewContact.Email = "andre@heaven.com";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "Jan Franchipan";
            NewContact.Email = "jan@eigenbelang.be";
            _AddressBook.Add(NewContact);

            NewContact = new Contact();
            NewContact.Name = "Josephine DePin";
            NewContact.Email = "jospin@proximus.be";
            NewContact.PhoneNumber = "054/44.87.26";
            NewAddress = new Address("Weverijstraat 12", "9500", "Geraardsbergen");
            NewContact.Address = NewAddress;
            _AddressBook.Add(NewContact);

            _AddressBook.Save();
        }
    }
}