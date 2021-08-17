using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GenerateJson.Base;
using GenerateJson.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenerateJson
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.Json)]
        public async Task GetSingleUser()
        {
            using HttpClient client = new() {BaseAddress = new Uri("https://jsonplaceholder.typicode.com")};

            User user = await client.GetFromJsonAsync<User>("users/1");
            
            Assert.IsTrue(user.Name == "Leanne Graham");
            Assert.IsTrue(user.Company.Name == "Romaguera-Crona");
            Assert.IsTrue(user.Address.Geo.Latitude == "81.1496");
            Assert.IsTrue(user.Address.Geo.Longitude == "-37.3159");

        }

        [TestMethod]
        [TestTraits(Trait.Json)]
        public async Task GetAllUser()
        {
            using HttpClient client = new() { BaseAddress = new Uri("https://jsonplaceholder.typicode.com") };

            List<User> userList = await client.GetFromJsonAsync<List<User>>("users");
            Assert.AreEqual(userList.Count,10);

        }

    }
}
