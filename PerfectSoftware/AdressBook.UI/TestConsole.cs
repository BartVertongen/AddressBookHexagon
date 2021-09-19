//By Bart Vertongen copyright 2021

using System.Collections.Generic;
using System.Threading.Tasks;
using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.UI
{
    public interface IInputIterator
    {
        string GetInput();
    }

    /// <summary>
    /// This is a class that allows you to set all the future inputs for the Test Console.
    /// Everytime you call 'GetInput' the next item is returned.
    /// </summary>
    public class InputIterator
    {
        private static int iCounter = 1;
        private readonly string _Filter;
        private readonly string _Name;
        private readonly string _Street;
        private readonly string _PostalCode;
        private readonly string _Town;
        private readonly string _Phone;
        private readonly string _Email;

        public InputIterator(string filter, string name, string street, string postalcode, string town, string phone, string email)
        {
            _Filter = filter;
            _Name = name;
            _Street = street;
            _PostalCode = postalcode;
            _Town = town;
            _Phone = phone;
            _Email = email;
        }

        private bool CanGiveFilter()
        {
            if (iCounter == 1 && _Filter != null)
            {
                iCounter++;
                return true;
            }
            else
            {
                iCounter++;
                return false;
            }              
        }

        private bool CanGiveName()
        {
            if (iCounter == 2 && _Name != null)
            {
                iCounter++;
                return true;
            }
            else
            {
                iCounter++;
                return false;
            }
        }

        private bool CanGiveStreet()
        {
            if (iCounter == 3 && _Street != null)
            {
                iCounter++;
                return true;
            }
            else
            {
                iCounter++;
                return false;
            }
        }

        private bool CanGivePostalCode()
        {
            if (iCounter == 4 && _PostalCode != null)
            {
                iCounter++;
                return true;
            }
            else
            {
                iCounter++;
                return false;
            }
        }

        private bool CanGiveTown()
        {
            if (iCounter == 5 && _Town != null)
            {
                iCounter++;
                return true;
            }
            else
            {
                iCounter++;
                return false;
            }
        }

        private bool CanGivePhone()
        {
            if (iCounter == 6 && _Phone != null)
            {
                iCounter++;
                return true;
            }
            else
            {
                iCounter++;
                return false;
            }
        }

        private bool CanGiveEmail()
        {
            if (iCounter == 7 && _Email != null)
            {
                iCounter++;
                return true;
            }
            else
            {
                iCounter++;
                return false;
            }
        }

        public string GetInput()
        {
            if (this.CanGiveFilter())
                return _Filter;               
            else if (this.CanGiveName())
                return _Name;
            else if (this.CanGiveStreet())
                return _Street;
            else if (this.CanGivePostalCode())
                return _PostalCode;
            else if (this.CanGiveTown())
                return _Town;
            else if (this.CanGivePhone())
                return _Phone;
            else if (this.CanGiveEmail())
                return _Email;
            else
                return null;
        }
    }

    public class TestConsole : IConsole
    {
        List<string> _Contents;
        private IInputIterator _InputIterator;

        public TestConsole(IInputIterator inputs)
        {
            _InputIterator = inputs;
            _Contents = new List<string>();
        }

        public string ReadLine()
        {
            if (_InputIterator != null)
            {
                string ReturnVal = _InputIterator.GetInput();
                this.Write(ReturnVal);
                return ReturnVal;
            }
            else
            {
                //Thread.Sleep(5000);
                Task.Delay(5000);
                return null;
            }
        }

        public void Write(string output = "")
        {
            if (_Contents.Count == 0)
                _Contents.Add(output);
            else
                _Contents[_Contents.Count - 1] += output;
        }

        public void WriteLine(string output = "")
        {
            this.Write(output);
            _Contents.Add("");
        }
    }
}