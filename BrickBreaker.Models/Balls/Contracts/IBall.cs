namespace BrickBreaker.Models.Balls.Contracts
{
    using BrickBreaker.Models.Positions;

    public interface IBall
    {
        string WreckingBall { get; }

        void CheckBorderCollision();

        Position Position { get; }

        string BallSymbol { get; }

        void Move();

        void ChangeDirectionForY();

        void ChangeDirectionForX();

        void ResetCurrentPositions();
    }
}
