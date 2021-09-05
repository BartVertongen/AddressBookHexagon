
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PS.AddressBook.Business;


namespace UseCasesTestConsole
{
    /// <summary>
    /// UseCase4: Delete a Contact.
    /// </summary>
    public class UseCase4
    {
        private AddressBook _AddressBook;
        private List<ContactLine> _ResultList;
        private string _Filter, _SelectedName;

        public UseCase4()
        {
            this.PreCondition();
        }

        public void Execute()
        {
            this.Step1();
            this.Step2();
            this.Step3();
            this.Step4();
            this.Step5();
            this.Step6();
        }

        /// <summary>
        /// You need an AddressBook with Contacts.
        /// </summary>
        public void PreCondition()
        {
            _AddressBook = new AddressBook();
            _AddressBook.XmlFile = "AddressBookUseCase4.xml";
            if (File.Exists(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile))
            {
                File.Delete(Environment.CurrentDirectory + "\\" + _AddressBook.XmlFile);
            }
            this.CreateAddressBookUseCase4();
            _Filter = "";
        }

        /// <summary>
        /// START:TRIGGER: USER asks for an overview of the adress Book Contacts with possible filtering.
        /// STEP1: 
        /// </summary>
        public void Step1()
        {
            Console.WriteLine($"UseCase4: Delete a Contact.");
            Console.Write($"Give in the filter you want to select the Contact: ");
        }

        /// <summary>
        /// The SYSTEM gets all Contacts from the DB passing the filter.
        /// </summary>
        public void Step2()
        {
            _Filter = Console.ReadLine();
            Console.WriteLine();
            this._ResultList = _AddressBook.GetOverview(_Filter);
        }

        /// <summary>
        /// STEP3: The System shows all possible Contacts.
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

                foreach (ContactLine oContactLn in this._ResultList)
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

        /// <summary>
        /// STEP4: The USER selects the Contact he wants to Delete.
        /// </summary>
        public void Step4()
        {
            if (this._ResultList.Count == 0)
            {
                Console.WriteLine("There is nothing to Delete, the AddressBook is empty!");
            }
            else
            {
                string sID;

                Console.Write("Give in the Id of the Contact you want to Delete: ");
                sID = Console.ReadLine();
                Console.WriteLine();
                bool IsIntegerString = sID.All(char.IsDigit);
                if (!IsIntegerString)
                { 
                    Console.WriteLine("You did not give in a integer.");
                    return;
                }
                else if (int.TryParse(sID, out int iID) == false)
                {
                    Console.WriteLine("We could not Parse the input as an integer!");
                    return;
                }                    
                else if (iID < 1 || iID > this._ResultList.Count)
                {
                    Console.WriteLine("You did not give in a valid ID!");
                    return;
                }
                else
                {
                    this._SelectedName = this._ResultList[iID-1].Name;
                }                  
            }
        }

        /// <summary>
        /// STEP 5:  The SYSTEM adapts the AddressBook, does the Delete.
        /// </summary>
        public void Step5()
        {
            if (string.IsNullOrEmpty(this._SelectedName))
            {
                Console.WriteLine("There is no Contact selected, nothing will happen!");
            }
            else
            {
                this._AddressBook.Delete(this._SelectedName);
            }
        }

        /// <summary>
        /// STEP 6:  The System removes the Contact from the DB.
        /// </summary>
        public void Step6()
        {
            if (string.IsNullOrEmpty(this._SelectedName))
            {
                Console.WriteLine("There is no Contact selected, nothing will happen!");
            }
            else
            {
                this._AddressBook.Save();
            }
        }

        private void CreateAddressBookUseCase4()
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