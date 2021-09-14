
using System;


namespace UseCasesTestConsole
{
    class Program
    {
        static UseCase1 oUseCase1;
        static UseCase4 oUseCase4;

        static void Main(string[] args)
        {
            Console.WriteLine("Here we test all the UseCases!");

            oUseCase1 = new UseCase1();
            oUseCase1.Execute();

            oUseCase4 = new UseCase4();
            oUseCase4.Execute();
        }
    }
}