//By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application.Adapters
{
    class AddressBookDTOAdapter : IAddressBookDTO
    {
        IList<ContactDTO> _Adaptee;

        public AddressBookDTOAdapter(IList<ContactDTO> adaptee)
        {
            _Adaptee = adaptee;
        }

        public ContactDTO this[int index]
        {
            get => _Adaptee[index];
            set => throw new System.NotImplementedException();
        }

        public int Count
        {
            get => _Adaptee.Count;
        }

        public bool IsReadOnly
        {
            get => false;
        }

        public void Add(ContactDTO item)
        {
            _Adaptee.Add(item);
        }

        public void Clear()
        {
            _Adaptee.Clear();
        }

        public bool Contains(ContactDTO item)
        {
            return _Adaptee.Contains(item);
        }

        public void CopyTo(ContactDTO[] array, int arrayIndex)
        {
            _Adaptee.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ContactDTO> GetEnumerator()
        {
            return _Adaptee.GetEnumerator();
        }

        public int IndexOf(ContactDTO item)
        {
            return _Adaptee.IndexOf(item);
        }

        public void Insert(int index, ContactDTO item)
        {
            _Adaptee.Insert(index, item);
        }

        public bool Remove(ContactDTO item)
        {
            return _Adaptee.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _Adaptee.RemoveAt(index);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Adaptee.GetEnumerator();
        }
    }
}