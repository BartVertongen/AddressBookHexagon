//By Bart Vertongen copyright 2021

using System.Collections.Generic;
using System.Threading.Tasks;
using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.Business
{
    public class TestConsole : IConsole
    {
        List<string> _Contents;
        public static string UserInput = null;

        public TestConsole()
        {
            _Contents = new List<string>();
        }

        public string ReadLine()
        {
            string ReturnVal;

            if (TestConsole.UserInput != null)
            {
                this.Write(UserInput);
                ReturnVal = UserInput;
                UserInput = null;
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