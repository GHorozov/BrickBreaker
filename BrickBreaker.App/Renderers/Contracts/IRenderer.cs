namespace BrickBreaker.App.Renderers.Contracts
{
    using BrickBreaker.Data.Data.Contracts;
    using BrickBreaker.Models.Balls.Contracts;
    using BrickBreaker.Models.Bricks.Contracts;
    using BrickBreaker.Models.Platforms.Contracts;
    using BrickBreaker.Models.Players.Contracts;
    using BrickBreaker.Models.Positions.Contracts;
    using OutputProvider.Contracts;

    public interface IRenderer
    {
        void RenderMainMenu(IOutputProvider outputProvider);
        void RenderMenuErrorInput(IOutputProvider outputProvider, string error);
        void RenderNameInputMenu(IOutputProvider outputProvider);
        void RenderResults(IOutputProvider outputProvider, string results);
        void RenderGoodby(IOutputProvider outputProvider);
        void RenderPlayground(IOutputProvider outputProvider, IPlayer player, IBrick bricks, IPlatform platform, IBall ball, IData data, int playerLives);
        void RenderPlatform(IOutputProvider outputProvider, IPlatform platform, IPosition position);
        void RenderBallMove(IOutputProvider outputprovider, IBall ball);
        void RenderCurrentScore(IOutputProvider outputProvider, IPlayer player, IData data, int playerLives);
        void RenderBallInitialPosition(IOutputProvider outputProvider, IBall ball);
        void RenderGameOver(IOutputProvider outputProvider, IBrick bricks, IData data, IPlayer player);
        void RenderWin(IOutputProvider outputProvider);
    }
}
