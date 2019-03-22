namespace BrickBreaker.App
{
    using System;
    using InputProvider;
    using InputProvider.Contracts;
    using OutputProvider;
    using OutputProvider.Contracts;
    using Renderers.Contracts;
    using Renderers;
    using Engines;
    using BrickBreaker.Data.Data.Contracts;
    using BrickBreaker.Data.Data;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            IInputProvider inputProvider = new ConsoleInputProvider();
            IOutputProvider outputProvider = new ConsoleOutputProvider();
            IRenderer renderer = new ConsoleRenderer();
            IData data = new Data();

            var engine = new Engine(inputProvider, outputProvider, renderer, data);
            engine.Initiate();
            engine.Run();
        }
    }
}
