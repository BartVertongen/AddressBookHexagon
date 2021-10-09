


namespace PS.AddressBook.Hexagon.Framework.Console
{
    /// <summary>
    /// This is a class that allows you to set all the future inputs for the Test Console.
    /// Every time you call 'GetInput' the next item is returned.
    /// </summary>
    public class InputIterator: IInputIterator
    {
        private static int iCounter = 1;
        private readonly string _Filter;
        private bool bFilterProcessed = false;
        private readonly string _Selection;
        private bool bSelectionProcessed = false;
        private readonly string _Name;
        private bool bNameProcessed = false;
        private readonly string _Street;
        private bool bStreetProcessed = false;
        private readonly string _PostalCode;
        private bool bPostalCodeProcessed = false;
        private readonly string _Town;
        private bool bTownProcessed = false;
        private readonly string _Phone;
        private bool bPhoneProcessed = false;
        private readonly string _Email;
        private bool bEmailProcessed = false;

        public InputIterator(string filter, string selection,
                string name, string street, string postalcode, string town, string phone, string email)
        {
            iCounter = 1;
            _Filter = filter;
            _Selection = selection;
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
                bFilterProcessed = true;
                return true;
            }
            else if (iCounter == 1 && _Filter == null)
            {
                iCounter++;
                bFilterProcessed = true;
                return false;
            }
            else
                return false;
        }

        private bool CanGiveSelection()
        {
            if (iCounter == 2 && _Selection != "-1")
            {
                iCounter++;
                bSelectionProcessed = true;
                return true;
            }
            else if (iCounter == 2 && _Selection == "-1")
            {
                iCounter++;
                bSelectionProcessed = true;
                return false;
            }
            else
                return false;
        }

        private bool CanGiveName()
        {
            if (iCounter == 3 && _Name != null)
            {
                iCounter++;
                bNameProcessed = true;
                return true;
            }
            else if (iCounter == 3 && _Name == null)
            {
                iCounter++;
                bNameProcessed = true;
                return false;
            }
            else
                return false;
        }

        private bool CanGiveStreet()
        {
            if (iCounter == 4 && _Street != null)
            {
                iCounter++;
                bStreetProcessed = true;
                return true;
            }
            else if (iCounter == 4 && _Street == null)
            {
                iCounter++;
                bStreetProcessed = true;
                return false;
            }
            else
                return false;
        }

        private bool CanGivePostalCode()
        {
            if (iCounter == 5 && _PostalCode != null)
            {
                iCounter++;
                this.bPostalCodeProcessed = true;
                return true;
            }
            else if (iCounter == 5 && _PostalCode == null)
            {
                iCounter++;
                this.bPostalCodeProcessed = true;
                return false;
            }
            else
                return false;
        }

        private bool CanGiveTown()
        {
            if (iCounter == 6 && _Town != null)
            {
                iCounter++;
                this.bTownProcessed = true;
                return true;
            }
            else if (iCounter == 6 && _Town == null)
            {
                iCounter++;
                this.bTownProcessed = true;
                return false;
            }
            else
                return false;
        }

        private bool CanGivePhone()
        {
            if (iCounter == 7 && _Phone != null)
            {
                iCounter++;
                this.bPhoneProcessed = true;
                return true;
            }
            else if (iCounter == 7 && _Phone == null)
            {
                iCounter++;
                this.bPhoneProcessed = true;
                return false;
            }
            else
                return false;
        }

        private bool CanGiveEmail()
        {
            if (iCounter == 8 && _Email != null)
            {
                iCounter++;
                this.bEmailProcessed = true;
                return true;
            }
            else if (iCounter == 8 && _Email == null)
            {
                iCounter++;
                this.bEmailProcessed = true;
                return true;
            }
            else
                return false;
        }

        public string GetInput()
        {
            if (!bFilterProcessed && this.CanGiveFilter())
                return _Filter;
            else if (!bSelectionProcessed && this.CanGiveSelection())
                return _Selection;
            else if (!bNameProcessed && this.CanGiveName())
                return _Name;
            else if (!bStreetProcessed && this.CanGiveStreet())
                return _Street;
            else if (!bPostalCodeProcessed && this.CanGivePostalCode())
                return _PostalCode;
            else if (!bTownProcessed && this.CanGiveTown())
                return _Town;
            else if (!bPhoneProcessed && this.CanGivePhone())
                return _Phone;
            else if (!bEmailProcessed && this.CanGiveEmail())
                return _Email;
            else
                return null;
        }
    }
}