namespace BrickBreaker.Models.Balls
{
    using System;
    using BrickBreaker.Models.Positions;
    using BrickBreaker.Models.Balls.Contracts;

    public class Ball : IBall
    {
        private const string WreckingBallSymbol = "@";
        private int initialXPosition = Console.WindowWidth / 2;
        private int initialYPosition = Console.WindowHeight - 5;
        private const int TopLineRowsHeight = 2;
        private Position position;
        private int[] verticalDirection;
        private int[] horizontalDirection;
        private int currentDirectionX;
        private int currentDirectionY;

        public Ball()
        {
            this.WreckingBall = WreckingBallSymbol;
            this.position = new Position(initialXPosition, initialYPosition);
            this.verticalDirection = new int[2] { -1, 1 };
            this.horizontalDirection = new int[2] { -1, 1 };
            this.currentDirectionX = 0;
            this.currentDirectionY = 0;
        }

        public string WreckingBall { get; }

        public string BallSymbol => WreckingBallSymbol;

        public Position Position => this.position;

        public void CheckBorderCollision()
        {
            if (position.X <= 0)
            {
                this.ChangeDirectionForX();
            }

            if (position.X >= Console.WindowWidth - 1)
            {
                this.ChangeDirectionForX();
            }

            if (position.Y <= TopLineRowsHeight)
            {
                this.ChangeDirectionForY();
            }

            if (position.Y >= Console.WindowHeight - 1)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public void ChangeDirectionForX()
        {
            if (currentDirectionX == 0)
            {
                this.currentDirectionX = 1;
            }
            else
            {
                this.currentDirectionX = 0;
            }
        }

        public void ChangeDirectionForY()
        {
            if (currentDirectionY == 0)
            {
                this.currentDirectionY = 1;
            }
            else
            {
                this.currentDirectionY = 0;
            }
        }

        public void Move()
        {
            this.position.X += horizontalDirection[currentDirectionX];
            this.position.Y += verticalDirection[currentDirectionY];
        }

        public void ResetCurrentPositions()
        {
            this.position.X = initialXPosition;
            this.position.Y = initialYPosition;
            ChangeDirectionForY();
            this.currentDirectionX = 0;
            this.currentDirectionX = 0;
        }
    }
}
