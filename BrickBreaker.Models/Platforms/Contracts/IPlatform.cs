namespace BrickBreaker.Models.Platforms.Contracts
{
    using BrickBreaker.Models.Positions;

    public interface IPlatform
    {
        string PlatformBase { get; }

        char PlatformSymbol { get; }

        int PlatformLenght { get; }

        Position Position { get; }

        Position MoveLeft();

        Position MoveRight();
    }
}
