namespace BrickBreaker.Models.Bricks.Contracts
{
    public interface IBrick
    {
        int[,] Matrix { get; }

        string BrickSymbol { get; }

        int BrickCount { get; }

        void IncreaseBricksCount();

        void DecreaseBrickCount();
    }
}
