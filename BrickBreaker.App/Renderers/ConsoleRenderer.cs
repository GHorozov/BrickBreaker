namespace BrickBreaker.App.Renderers
{
    using System;
    using BrickBreaker.App.OutputMessages;
    using BrickBreaker.App.OutputProvider.Contracts;
    using BrickBreaker.App.Renderers.Contracts;
    using BrickBreaker.Data.Data.Contracts;
    using BrickBreaker.Models.Balls.Contracts;
    using BrickBreaker.Models.Bricks.Contracts;
    using BrickBreaker.Models.Platforms.Contracts;
    using BrickBreaker.Models.Players.Contracts;
    using BrickBreaker.Models.Positions.Contracts;

    public class ConsoleRenderer : IRenderer
    {
        private const string GameLogo = ".::BRICK BREAKER::.";
        private const string MenuItemNewGame = "NewGame";
        private const string MenuItemAllScores = "ShowAllScores";
        private const string MenuItemTopFiveScore = "ShowTopFiveScores";
        private const string MenuItemExit = "Exit";
        private const string ExecutionLine = "Enter command: ";
        private const char DeviderSymbol = '-';
        private const string TopLineDeviderSymbol = "-";
        private const string NameLine = "Enter your name: ";
        private const string GameOverString = "GameOver!";
        private const string WinGameString = "Win!";

        private const int IncrementIndexZero = 0;
        private const int IncrementIndexOne = 1;
        private const int IncrementIndexTwo = 2;
        private const int IncrementIndexThree = 3;
        private const int IncrementIndexFour = 4;
        private const int IncrementIndexFive = 5;
        private const int IncrementIndexSix = 6;
        private const int IncrementIndexSeven = 7;
        private const int NumberOfDevideSymbols = 40;
        private const int PositionOfPointsIndex = 58;
        private const int PositionOfLivesIndex = 79;
        private const int EmptySpaceLenght = 10;
        private const int PlayerStringLenght = 7;
        private const int BestScoreLenght = 10;
        private const int YourScoreLenght = 10;


        public ConsoleRenderer()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.SetWindowSize(100, 50);
            Console.BufferHeight = 50;
            Console.BufferWidth = 100;
        }

        public void RenderMainMenu(IOutputProvider outputProvider)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - GameLogo.Length / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo);
            outputProvider.WriteLine(GameLogo);
            outputProvider.WriteLine(string.Empty);
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - MenuItemNewGame.Length / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo+IncrementIndexTwo);
            outputProvider.WriteLine(MenuItemNewGame);
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - MenuItemAllScores.Length / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo + IncrementIndexThree);
            outputProvider.WriteLine(MenuItemAllScores);
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - MenuItemTopFiveScore.Length / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo + IncrementIndexFour);
            outputProvider.WriteLine(MenuItemTopFiveScore);
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - MenuItemExit.Length / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo + IncrementIndexFive);
            outputProvider.WriteLine(MenuItemExit);
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - NumberOfDevideSymbols / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo + IncrementIndexSix);
            outputProvider.WriteLine(new string(DeviderSymbol, NumberOfDevideSymbols));
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - ExecutionLine.Length, Console.WindowHeight / IncrementIndexTwo + IncrementIndexSeven);
            outputProvider.Write(ExecutionLine);
        }

        public void RenderMenuErrorInput(IOutputProvider outputProvider, string error)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - NumberOfDevideSymbols / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo);
            outputProvider.WriteLine(error);
        }

        public void RenderNameInputMenu(IOutputProvider outputProvider)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - NumberOfDevideSymbols/IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo);
            outputProvider.Write(NameLine);
        }

        public void RenderResults(IOutputProvider outputProvider, string results)
        {
            Console.Clear();
            var counter = 0;
            foreach (var item in results.Split(Environment.NewLine))
            {
                Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - IncrementIndexSeven, Console.WindowHeight / IncrementIndexTwo + counter++);
                outputProvider.WriteLine(item);
            }
            
        }

        public void RenderGoodby(IOutputProvider outputProvider)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - OutputMessages.SuccessMessages.GoodbuyMessage.Length/ IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo);
            outputProvider.WriteLine(SuccessMessages.GoodbuyMessage);
            Console.SetCursorPosition(0,0);
        }

        public void RenderPlayground(IOutputProvider outputProvider, IPlayer player, IBrick bricks, IPlatform platform, IBall ball, IData data, int playerLives)
        {
            Console.Clear();

            RenderCurrentScore(outputProvider, player, data, playerLives);

            Console.SetCursorPosition(IncrementIndexZero, IncrementIndexOne);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                outputProvider.Write(TopLineDeviderSymbol);
            }
            
            var matrix = bricks.Matrix;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if(matrix[i,j] != 0)
                    {
                        Console.SetCursorPosition(i,j);
                        outputProvider.Write(bricks.BrickSymbol);
                        bricks.IncreaseBricksCount();
                    }
                }
            }
            
            var platformBase = platform.PlatformBase;
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - platformBase.Length / IncrementIndexTwo, Console.WindowHeight - IncrementIndexTwo);
            outputProvider.WriteLine(platformBase);
        }

        public void RenderPlatform(IOutputProvider outputProvider, IPlatform platform, IPosition position)
        {
            Console.SetCursorPosition(0, position.Y);
            outputProvider.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(position.X, position.Y);
            var platformString = new string(platform.PlatformSymbol, platform.PlatformLenght);
            outputProvider.Write(platformString);
        }

        public void RenderBallMove(IOutputProvider outputprovider, IBall ball)
        {
            Console.SetCursorPosition(ball.Position.X, ball.Position.Y);
            outputprovider.Write(" ");
            ball.Move();
            Console.SetCursorPosition(ball.Position.X, ball.Position.Y);
            outputprovider.Write(ball.BallSymbol);
        }

        public void RenderCurrentScore(IOutputProvider outputProvider, IPlayer player, IData data, int playerLives)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                outputProvider.Write(" ");
            }
            var bestScore = data.GetBestScore();
            var currentLenght = 0;
            Console.SetCursorPosition(0, 0);
            outputProvider.Write($"Player: {player.Name}");
            currentLenght += (player.Name.Length + PlayerStringLenght + EmptySpaceLenght);
            Console.SetCursorPosition(currentLenght, 0);
            outputProvider.Write($"BestScore: {bestScore.ToString()}");
            currentLenght += (BestScoreLenght + bestScore.ToString().Length + EmptySpaceLenght);
            Console.SetCursorPosition(currentLenght, 0);
            outputProvider.Write($"YourScore: {player.Points}");
            currentLenght += (YourScoreLenght + player.Points.ToString().Length + EmptySpaceLenght);
            Console.SetCursorPosition(currentLenght, 0);
            outputProvider.Write($"Lives: {playerLives.ToString()}");
        }

        public void RenderBallInitialPosition(IOutputProvider outputProvider, IBall ball)
        {
            Console.SetCursorPosition(ball.Position.X, ball.Position.Y);
            outputProvider.Write(" ");
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo, Console.WindowHeight - IncrementIndexFour);
            outputProvider.Write(" ");
            ball.ResetCurrentPositions();
        }

        public void RenderGameOver(IOutputProvider outputProvider, IBrick bricks, IData data, IPlayer player)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - GameOverString.Length / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo);
            outputProvider.Write(GameOverString);
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - GameOverString.Length / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo + IncrementIndexTwo);
            outputProvider.Write(string.Format(OtherMessages.GameOverInfoBestScore, data.GetBestScore()));
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - GameOverString.Length / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo + IncrementIndexThree);
            outputProvider.Write(string.Format(OtherMessages.GameOverInfoYourScore, player.Points));
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - GameOverString.Length / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo + IncrementIndexFour);
            outputProvider.Write(string.Format(OtherMessages.GameOverInfoBricksLeft, bricks.BrickCount));
            Console.SetCursorPosition(0,0);
        }

        public void RenderWin(IOutputProvider outputProvider)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / IncrementIndexTwo - GameLogo.Length / IncrementIndexTwo, Console.WindowHeight / IncrementIndexTwo);
            outputProvider.Write(WinGameString);
        }
    }
}
