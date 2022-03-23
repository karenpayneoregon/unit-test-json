using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ContainerLibrary.Classes;
using Json.Library.Extensions;
using JsonTestProject.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonTestProject
{
    /// <summary>
    /// Based off
    /// https://stackoverflow.com/questions/71580697/how-to-read-a-text-file-and-sort-it-by-value
    /// Which I did not respond as the user is not to a level to understand this code.
    /// </summary>
    [TestClass]
    public partial class UserScores : TestBase
    {

        [TestMethod]
        [TestTraits(Trait.Scores)]
        public void WriteScores()
        {
            List<Player> list = new()
            {
                new() { Name = "Jim", Score = 98 },
                new() { Name = "Mike", Score = 96 },
                new() { Name = "Jim", Score = 80},
                new() { Name = "Mike", Score = 56},
                new() { Name = "Mike", Score = 66}
            };

            var (success, exception) = list.JsonToFile(FileName);
            Assert.AreEqual(success, true);
        }

        [TestMethod] 
        [TestTraits(Trait.Scores)]
        public void ReadOnePlayer()
        {
            var json = File.ReadAllText(FileName);
            List<Player> players = json.JSonToList<Player>();

            List<Player> mikeHighToLow = players
                .Where(player => player.Name == "Mike")
                .OrderByDescending(player => player.Score).ToList();

            foreach (var player in mikeHighToLow)
            {
                Console.WriteLine($"{player.Score}");
            }
        }

        [TestMethod]
        [TestTraits(Trait.Scores)]
        public void GroupReadPlayers()
        {
            List<PlayerGrouped> groupedPlayers = GetPlayerHighScore();

            foreach (var item in groupedPlayers)
            {
                Console.WriteLine(item.Name);
                foreach (var player in item.List)
                {
                    Console.WriteLine($"\t{player.Score}");
                }
            }

        }

    }

}