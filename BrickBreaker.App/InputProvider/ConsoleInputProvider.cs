namespace BrickBreaker.App.InputProvider
{
    using System;
    using Contracts;

    public class ConsoleInputProvider : IInputProvider
    {
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
