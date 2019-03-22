namespace BrickBreaker.Models.Players
{
    using System;
    using Contracts;

    public class Player : IPlayer
    {
        private string name;

        public Player(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("You have to enter your name first!");
                }

                this.name = value;
            }
        }

        public int Points { get; private set; }

        public void AddPoints(int points)
        {
            this.Points += points;
        }

        public void IncreasePoints()
        {
            this.Points++;
        }
        
    }
}
