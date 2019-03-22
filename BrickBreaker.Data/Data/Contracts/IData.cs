namespace BrickBreaker.Data.Data.Contracts
{
    using BrickBreaker.Models.Players.Contracts;
    using System.Collections.Generic;

    public interface IData
    {
        IReadOnlyDictionary<string, int> Results { get; }

        void AddNewResult(IPlayer player);

        string GetAllResults();
       
        string GetTopFiveResult();

        bool IsNameExists(string name);

        int GetBestScore();
    }
}
