namespace BrickBreaker.App.InputProvider.Contracts
{
    using System;

    public interface IInputProvider
    {
        string ReadLine();
       
        ConsoleKeyInfo ReadKey();
    }
}
