namespace BrickBreaker.Models.Platforms
{
    using System;
    using BrickBreaker.Models.Positions;
    using BrickBreaker.Models.Platforms.Contracts;

    public class Platform : IPlatform
    {
        private char platformSymbol = '#';
        private const int platformLenght = 10;
        private const int PlatformBorderOffset = 2;
        private const int PositionOffset = 1;
        private int initialXPosition = Console.WindowWidth / 2 - platformLenght / 2;
        private int initialYPosition = Console.WindowHeight - 2;
        private Position position;

        public Platform()
        {
            this.PlatformBase = new string(platformSymbol, platformLenght);
            this.position = new Position(initialXPosition, initialYPosition);
        }

        public string PlatformBase { get; }
        public char PlatformSymbol => platformSymbol;
        public int PlatformLenght => platformLenght;

        public Position Position => this.position;

        public Position MoveLeft()
        {
            if(this.position.X > PositionOffset)
            {
                this.position.X -= PlatformBorderOffset;
                return position;
            }
            else
            {
                return null;
            }
        }

        public Position MoveRight()
        {
            if(this.position.X + platformLenght < Console.WindowWidth - PositionOffset)
            {
                this.position.X += PlatformBorderOffset;
                return position;
            }
            else
            {
                return null;
            }
        }
    }
}
