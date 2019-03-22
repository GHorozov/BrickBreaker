namespace BrickBreaker.Models.Positions
{
    using BrickBreaker.Models.Positions.Contracts;

    public class Position : IPosition
    {
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
