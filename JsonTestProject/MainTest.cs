using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ConfigurationHelper;
using ContainerLibrary.Classes;
using Json.Library;
using Json.Library.Extensions;
using JsonTestProject.Base;
using JsonTestProject.Classes;
using JsonTestProject.FakeGeneratorClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        /// <summary>
        /// Deserialize file containing list of <see cref="Contact"/> from a file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Deserialize)]
        public void ContactsDeserialize()
        {
            var json = File.ReadAllText(ReadFileName);
            List<Contact> contacts = json.JSonToList<Contact>();
            Assert.AreEqual(contacts.Count, 91);
        }
        /// <summary>
        /// Serialize list of <see cref="Contact"/> to a file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Serialize)]
        public void ContactSerialize()
        {
            List<Contact> contacts = File.ReadAllText(ReadFileName).JSonToList<Contact>();
            var (success, exception) = contacts.JsonToFile(ContactSerializeFileName);
            Assert.IsTrue(File.Exists(ContactSerializeFileName) && success);
        }

        /// <summary>
        /// Serialize list of <see cref="Customer"/> to a file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Serialize)]
        public void CustomerSerialize()
        {
            var json = File.ReadAllText(ReadFileName);
            List<Customer> customers = json.JSonToList<Customer>();

            var singleCustomer = customers.FirstOrDefault(customer => customer.Identifier == 2);


            singleCustomer.CompanyName = singleCustomer.CompanyName.ToLower();
            Assert.IsTrue(singleCustomer.CompanyName == "ana trujillo emparedados y helados");
            var (success, exception) = customers.JsonToFile(CustomersSerializeFileName);
            Assert.IsTrue(File.Exists(CustomersSerializeFileName) && success);
        }
        /// <summary>
        /// Deserialize file containing list of <see cref="Customer"/> from a file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Deserialize)]
        public void CustomersDeserialize()
        {
            var json = File.ReadAllText(ReadFileName);
            List<Customer> customers = json.JSonToList<Customer>();
            Assert.AreEqual(customers.Count, 50);
        }

        /// <summary>
        /// Demo for deserializing unix epoch date
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// This test is slow and can only be faster by mocking data.
        /// Get-ComputerInfo | select BiosReleaseDate,OsLocalDateTime,OsLastBootUpTime,OsUptime
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.Deserialize)]
        public async Task UnixEpochDateTimeDeserialize()
        {
            try
            {
                var computerDetails = await PowerShellOperations.GetPartialComputerInformationTask(ComputerDetailsFileName);
                Assert.IsNotNull(computerDetails);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected no exception, with : {ex.Message}");
            }

        }

        /// <summary>
        /// This test does not use System.Text.Json, instead Microsoft.Extensions.Configuration
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.AppSettings)]
        public void ConnectionStringTest()
        {
            var result = Helper.ConnectionString();
            Assert.IsTrue(result == NorthWindConnectionString);
        }
        /// <summary>
        /// This test does not use System.Text.Json, instead Microsoft.Extensions.Configuration
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.AppSettings)]
        public void UserSettingTest()
        {
            var result = Helpers.UserSettings();
            Assert.IsTrue(result.UserName == "jandi" && result.Password == "jandi123" && result.Server == "abc");
        }

        /// <summary>
        /// Example to test reading json from a web address
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestTraits(Trait.JsonPlaceHolder)]
        public async Task JsonPlaceHolder_Get_User()
        {
            using HttpClient client = new()
            {
                BaseAddress = PlaceHolderAddress
            };

            // Get the user information.
            var user = await client.GetFromJsonAsync<User>("users/1");
            Console.WriteLine($"Id: {user.Id}");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Username: {user.Username}");
            Console.WriteLine($"Email: {user.Email}");

            // Post a new user.
            HttpResponseMessage response = await client.PostAsJsonAsync("users", user);
            Console.WriteLine($"{(response.IsSuccessStatusCode ? "Success" : "Error")} - {response.StatusCode}");
        }

        /// <summary>
        /// https://fakerapi.it/en/
        /// Not a test just yet TODO
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestTraits(Trait.JsonPlaceHolder)]
        public async Task CreateFakeListOfPeopleTest()
        {
            List<Datum> list = new();

            using HttpClient client = new()
            {
                BaseAddress = new Uri("https://fakerapi.it/api/v1/")
            };

            var fakeMales = await client.GetFromJsonAsync<FakePersonRoot>(FakerOperations.CreateMalePeople(10, new DateTime(1956,1,1)));
            var fakeFemales = await client.GetFromJsonAsync<FakePersonRoot>(FakerOperations.CreateFemalePeople(10, new DateTime(1945,1,1)));

            foreach (var item in fakeMales.data)
            {
                list.Add(item);
            }

            foreach (var item in fakeFemales.data)
            {
                list.Add(item);
            }

            list.Shuffle();

            // since both list have the same id's we need to fix that
            for (int index = 0; index < list.Count; index++)
            {
                list[index].id = index + 1;
            }

            
            foreach (var item in list)
            {
                Console.WriteLine($"{item.id} {item.FirstName} {item.LastName} {item.Gender} {item.BirthDate:d}");
                Console.WriteLine($"\t{item.Address.street} {item.Address.city} {item.Address.country} {item.Address.zipcode}");
            }
        }


        /// <summary>
        /// Example to test reading json from a web address
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestTraits(Trait.JsonPlaceHolder)]
        public async Task JsonPlaceHolder_Get_Posts()
        {
            using HttpClient client = new()
            {
                BaseAddress = PlaceHolderAddress
            };

            var userIdentifier = 1;
            var expected = 10;

            var posts = await client.GetFromJsonAsync<List<Posts>>("posts");
            var subset = posts!.Where(post => post.UserIdentifier == userIdentifier).ToList();
            Assert.AreEqual(subset.Count, expected, $"Expected count of {expected}");

        }

        /// <summary>
        /// Allow comments and trailing commas
        /// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-invalid-json?pivots=dotnet-5-0#allow-comments-and-trailing-commas
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.JsonPlaceHolder)]
        public void IgnoreCommentsInJson()
        {
            var jsonString = File.ReadAllText(WeatherFileName); ;
            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            };
            var weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(jsonString, options);
            Assert.IsTrue(weatherForecast.Summary == "Hot");
        }

    }
}

