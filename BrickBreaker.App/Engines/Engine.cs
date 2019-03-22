namespace BrickBreaker.App.Engines
{
    using InputProvider.Contracts;
    using OutputProvider.Contracts;
    using Contracts;
    using Renderers.Contracts;
    using BrickBreaker.Models.Players.Contracts;
    using System;
    using BrickBreaker.App.OutputMessages;
    using BrickBreaker.Data.Data.Contracts;
    using BrickBreaker.Models.Players;
    using System.Threading;
    using BrickBreaker.Models.Bricks.Contracts;
    using BrickBreaker.Models.Platforms.Contracts;
    using BrickBreaker.Models.Bricks;
    using BrickBreaker.Models.Platforms;
    using BrickBreaker.Models.Balls;
    using BrickBreaker.Models.Balls.Contracts;

    public class Engine : IEngine
    {
        private const int MaximusNameSymbols = 15;
        private const int startMatrixRow = 2;
        private int endMatrixRow = Console.WindowHeight - 2;
        private const int startMatrixCol = 2;
        private int endMatrixCol = Console.WindowWidth - 2;
        private int sleepTime = 200;

        private IInputProvider inputProvider;
        private IOutputProvider outputProvider;
        private IRenderer renderer;
        private IData data;
        private IPlayer player;
        private IBrick bricks;
        private IPlatform platform;
        private IBall ball;
        private int playerLives;

        public Engine(IInputProvider inputProvider, IOutputProvider outputProvider, IRenderer renderer, IData data)
        {
            this.inputProvider = inputProvider;
            this.outputProvider = outputProvider;
            this.renderer = renderer;
            this.data = data;
            this.bricks = new Brick();
            this.platform = new Platform();
            this.ball = new Ball();
            this.playerLives = 3;
        }

        public void Initiate()
        {
            var isInitiationDone = false;
            while (true)
            {
                if (isInitiationDone)
                {
                    break;
                }

                try
                {
                    this.renderer.RenderMainMenu(this.outputProvider);
                    var inputCommand = inputProvider.ReadLine().ToLower();
                    switch (inputCommand)
                    {
                        case "newgame":
                            this.renderer.RenderNameInputMenu(this.outputProvider);
                            var inputName = this.inputProvider.ReadLine();
                            ValidateName(inputName);
                            this.player = new Player(inputName);
                            this.data.AddNewResult(this.player);
                            isInitiationDone = true;
                            break;
                        case "showallscores":
                            var allResult = this.data.GetAllResults();
                            this.renderer.RenderResults(this.outputProvider, allResult);
                            Thread.Sleep(4000);
                            break;
                        case "showtopfivescores":
                            var topFiveResult = this.data.GetTopFiveResult();
                            this.renderer.RenderResults(this.outputProvider, topFiveResult);
                            Thread.Sleep(4000);
                            break;
                        case "exit":
                            this.renderer.RenderGoodby(this.outputProvider);
                            Environment.Exit(1);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    this.renderer.RenderMenuErrorInput(this.outputProvider, ex.Message);
                    Thread.Sleep(2000);
                }
            }
        }

        public void Run()
        {
            Console.CursorVisible = false;
            this.renderer.RenderPlayground(this.outputProvider, this.player, this.bricks, this.platform, this.ball, this.data, this.playerLives);
           
            var isGameOver = false;
            while (this.bricks.BrickCount != 0)
            {
                if (isGameOver) break;

                if(this.player.Points % 10 == 0)
                {
                    this.sleepTime--;
                }

                try
                {
                    this.ball.CheckBorderCollision();
                }
                catch (IndexOutOfRangeException)
                {
                    this.playerLives--;
                    if (this.playerLives == 0)
                    {
                        if (this.player.Points > 0)
                        {
                            this.data.AddNewResult(this.player);//
                        }

                        isGameOver = true;
                    }
                    this.renderer.RenderCurrentScore(this.outputProvider, this.player, this.data, this.playerLives);
                    this.renderer.RenderBallInitialPosition(this.outputProvider, this.ball);
                }

                this.CheckForFlaformCollision();
                this.ChechForBrickCollision();
                this.MoveBall();
                Thread.Sleep(sleepTime);
                this.MovePlatform();
            }

            if (isGameOver)
            {
                this.renderer.RenderGameOver(this.outputProvider, this.bricks, this.data, this.player);
            }
            else
            {
                this.data.AddNewResult(this.player);
                this.renderer.RenderWin(this.outputProvider);
            }
        }

        private void ValidateName(string inputName)
        {
            if (string.IsNullOrWhiteSpace(inputName))
            {
                throw new ArgumentException(ErrorMessages.InvalidNameLenght);
            }
            if (inputName.Length > MaximusNameSymbols)
            {
                throw new ArgumentException(ErrorMessages.InvalidNameToLongSymbols);
            }
            if (this.data.IsNameExists(inputName))
            {
                throw new ArgumentException(ErrorMessages.InvalidAlreadyExists);
            }
        }

        private void MovePlatform()
        {
            while (Console.KeyAvailable)
            {
                var keyInput = inputProvider.ReadKey();

                if (keyInput.Key == ConsoleKey.LeftArrow || keyInput.Key == ConsoleKey.RightArrow)
                {
                    if (keyInput.Key == ConsoleKey.LeftArrow)
                    {
                        var position = this.platform.MoveLeft();
                        if (position != null)
                        {
                            this.renderer.RenderPlatform(this.outputProvider, this.platform, position);
                        }
                    }

                    if (keyInput.Key == ConsoleKey.RightArrow)
                    {
                        var position = this.platform.MoveRight();
                        if (position != null)
                        {
                            this.renderer.RenderPlatform(this.outputProvider, this.platform, position);
                        }
                    }
                }
            }
        }

        private void MoveBall()
        {
            this.renderer.RenderBallMove(this.outputProvider, this.ball);
        }

        private void CheckForFlaformCollision()
        {
            var IncrementDecrementIndex = 1;
            var leftPlatformSpace = 3;

            if (this.ball.Position.X >= this.platform.Position.X - IncrementDecrementIndex &&
                this.ball.Position.X <= this.platform.Position.X - IncrementDecrementIndex + leftPlatformSpace &&
                this.ball.Position.Y + IncrementDecrementIndex == this.platform.Position.Y)
            {
                this.ball.ChangeDirectionForX();
                this.ball.ChangeDirectionForY();
            }
            else if (this.ball.Position.X >= this.platform.Position.X  + leftPlatformSpace  &&
                    this.ball.Position.X <= this.platform.Position.X + this.platform.PlatformLenght  &&
                     this.ball.Position.Y + IncrementDecrementIndex == this.platform.Position.Y)
            {
                this.ball.ChangeDirectionForY();
            }
        }

        private void ChechForBrickCollision()
        {
            if (this.ball.Position.Y - 1 >= 0 &&
                this.bricks.Matrix[this.ball.Position.X, this.ball.Position.Y - 1] != 0)
            {
                this.bricks.Matrix[this.ball.Position.X, this.ball.Position.Y - 1] = 0;
                Console.SetCursorPosition(this.ball.Position.X, this.ball.Position.Y - 1);
                this.outputProvider.Write(" ");
                this.ball.ChangeDirectionForY();
                this.player.IncreasePoints();
                this.bricks.DecreaseBrickCount();
                this.renderer.RenderCurrentScore(this.outputProvider, this.player, this.data, this.playerLives);
            }

            if (this.ball.Position.Y + 1 <= Console.WindowHeight - 2 &&
                this.bricks.Matrix[this.ball.Position.X, this.ball.Position.Y + 1] != 0)
            {
                this.bricks.Matrix[this.ball.Position.X, this.ball.Position.Y + 1] = 0;
                Console.SetCursorPosition(this.ball.Position.X, this.ball.Position.Y + 1);
                this.outputProvider.Write(" ");
                this.ball.ChangeDirectionForY();
                this.player.IncreasePoints();
                this.bricks.DecreaseBrickCount();
                this.renderer.RenderCurrentScore(this.outputProvider, this.player, this.data, this.playerLives);
            }

            if (this.ball.Position.X + 1 <= Console.WindowWidth - 2 &&
                this.bricks.Matrix[this.ball.Position.X + 1, this.ball.Position.Y] != 0)
            {
                this.bricks.Matrix[this.ball.Position.X + 1, this.ball.Position.Y] = 0;
                Console.SetCursorPosition(this.ball.Position.X + 1, this.ball.Position.Y);
                this.outputProvider.Write(" ");
                this.ball.ChangeDirectionForX();
                this.player.IncreasePoints();
                this.bricks.DecreaseBrickCount();
                this.renderer.RenderCurrentScore(this.outputProvider, this.player, this.data, this.playerLives);
            }

            if (this.ball.Position.X - 1 >= 2 &&
                this.bricks.Matrix[this.ball.Position.X - 1, this.ball.Position.Y] != 0)
            {
                this.bricks.Matrix[this.ball.Position.X - 1, this.ball.Position.Y] = 0;
                Console.SetCursorPosition(this.ball.Position.X - 1, this.ball.Position.Y);
                this.outputProvider.Write(" ");
                this.ball.ChangeDirectionForX();
                this.player.IncreasePoints();
                this.bricks.DecreaseBrickCount();
                this.renderer.RenderCurrentScore(this.outputProvider, this.player, this.data, this.playerLives);
            }
        }
    }
}
