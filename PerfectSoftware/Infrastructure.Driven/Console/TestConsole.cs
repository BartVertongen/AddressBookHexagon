//By Bart Vertongen copyright 2021

using System.Collections.Generic;
using System.Threading.Tasks;


namespace PS.AddressBook.Framework.Console
{
    public class TestConsole : IConsole
    {
        private readonly List<string> _Contents;
        private readonly IInputIterator _InputIterator;

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
                _Contents.Add("");
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