using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerLibrary.Classes;
using Json.Library.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace JsonTestProject
{
    public partial class UserScores
    {
        private string FileName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "playerScore.json");

        [TestInitialize]
        public void Initialization()
        {
            if (TestContext.TestName == nameof(ReadOnePlayer))
            {
                var list = PlayersMocked();

                list.JsonToFile(FileName);
            }
        }

        private static List<Player> PlayersMocked() => new()
            {
                new() { Name = "Jim", Score = 98 },
                new() { Name = "Mike", Score = 96 },
                new() { Name = "Jim", Score = 80 },
                new() { Name = "Mike", Score = 56 },
                new() { Name = "Mike", Score = 66 }
            };


        private List<PlayerGrouped> GetPlayerHighScore()
        {
            var json = File.ReadAllText(FileName);
            List<Player> players = json.JSonToList<Player>();

            return players.GroupBy(p => p.Name)
                .Select(g => new PlayerGrouped(g.Key, g.OrderByDescending(p => p.Score).ToList()))
                .ToList();
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }

    }

}