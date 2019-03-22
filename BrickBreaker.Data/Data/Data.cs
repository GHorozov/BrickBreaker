namespace BrickBreaker.Data.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using BrickBreaker.Models.Players.Contracts;
    using Contracts;

    public class Data : IData
    {
        private const string NoResultString = "No results yet!";
        private static readonly string FilePath = "./../../scores.txt";
        private Dictionary<string, int> results;

        public Data()
        {
            this.results = new Dictionary<string, int>();
            this.results = GetReultsFromFile();
        }

        public IReadOnlyDictionary<string, int> Results => this.results;

        public void AddNewResult(IPlayer player)
        {
            if (!File.Exists(FilePath))
            {
                var newfile = File.Create(FilePath);
                newfile.Close();
            }

            if (!this.results.ContainsKey(player.Name))
            {
                this.results[player.Name] = 0;
            }

            this.results[player.Name] = player.Points;

            string stringResult = $"{player.Name}:{player.Points}" + Environment.NewLine;
            File.AppendAllText(FilePath, stringResult);
        }

        public string GetAllResults()
        {
            if (this.results.Count == 0)
            {
                return NoResultString;
            }

            var sb = new StringBuilder();
            var orderResults = this.results.OrderByDescending(x => x.Value);
            foreach (var item in orderResults)
            {
                sb.AppendLine($"{item.Key} - {item.Value}");
            }

            return sb.ToString().TrimEnd(); 
        }

        public string GetTopFiveResult()
        {
            if (this.results.Count == 0)
            {
                return NoResultString;
            }

            var sb = new StringBuilder();
            var orderResults = this.results.OrderByDescending(x => x.Value).Take(5);
            foreach (var item in orderResults)
            {
                sb.AppendLine($"{item.Key} - {item.Value}");
            }

            return sb.ToString().TrimEnd();
        }

        public bool IsNameExists(string name)
        {
            GetReultsFromFile();
            return this.results.ContainsKey(name);
        }

        public int GetBestScore()
        {
            if (this.results.Count == 0)
            {
                return 0;
            }

            var bestScore = this.results.OrderByDescending(x => x.Value).First().Value;

            return bestScore;
        }

        private Dictionary<string, int> GetReultsFromFile()
        {
            var resultDict = new Dictionary<string, int>(); 
            if(this.results.Count == 0)
            {
                if (File.Exists(FilePath))
                {
                    var lines = File.ReadAllLines(FilePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(":");
                        var name = parts[0];
                        var points = int.Parse(parts[1]);

                        if (!resultDict.ContainsKey(name))
                        {
                            resultDict[name] = 0;
                        }

                        resultDict[name] = points;
                    }
                }
            }

            return resultDict;
        }
    }
}
