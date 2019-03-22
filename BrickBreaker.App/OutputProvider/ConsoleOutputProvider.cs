namespace BrickBreaker.App.OutputProvider
{
    using System;
    using Contracts;

    public class ConsoleOutputProvider : IOutputProvider
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void Write(string text)
        {
            Console.Write(text);
        }
    }
}
