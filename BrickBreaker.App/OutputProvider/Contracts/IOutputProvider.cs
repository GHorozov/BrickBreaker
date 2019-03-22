namespace BrickBreaker.App.OutputProvider.Contracts
{
    public interface IOutputProvider
    {
        void WriteLine(string text);

        void Write(string text);
    }
}
