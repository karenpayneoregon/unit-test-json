using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using ContainerLibrary.Classes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonTestProject.Classes;


// ReSharper disable once CheckNamespace - do not change
namespace JsonTestProject
{
    public partial class MainTest
    {
        /// <summary>
        /// Used to read a json file which will be different for each test
        /// and is set in <see cref="Initialization"/>
        /// </summary>
        public string ReadFileName { get; set; }
        
        /// <summary>
        /// File to read for <see cref="Contact"/> test
        /// </summary>
        public string ContactSerializeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ContactsSerialized.json");
        
        /// <summary>
        /// File to read for <see cref="Customer"/> test
        /// </summary>
        public string CustomersSerializeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CustomersSerialized.json");
        
        /// <summary>
        /// File used for PowerShell operations
        /// </summary>
        public string ComputerDetailsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "computerDateInfo.json");
        public string WeatherFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Weather.json");

        /// <summary>
        /// Validate connection string in <see cref="ConnectionStringTest"/>
        /// </summary>
        public static string NorthWindConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=NorthWind2020;Integrated Security=True";

        /// <summary>
        /// For test methods <see cref="JsonPlaceHolder_Get_User"/> and <see cref="JsonPlaceHolder_Get_Posts"/>
        /// </summary>
        public Uri PlaceHolderAddress =>  new("https://jsonplaceholder.typicode.com");


        /// <summary>
        /// Code to run before specific test execute
        /// </summary>
        [TestInitialize]
        public void Initialization()
        {

            if (TestContext.TestName == nameof(ContactsDeserialize) || TestContext.TestName == nameof(ContactSerialize))
            {
                ReadFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "contacts.json");

                if (TestContext.TestName == nameof(ContactSerialize))
                {
                    if (File.Exists(ContactSerializeFileName))
                    {
                        File.Delete(ContactSerializeFileName);
                    }
                }

            }
            else if (TestContext.TestName == nameof(CustomersDeserialize) || TestContext.TestName == nameof(CustomerSerialize))
            {
                ReadFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "customers.json");

                if (File.Exists(CustomersSerializeFileName))
                {
                    File.Delete(CustomersSerializeFileName);
                }
            }

        }



        /// <summary>
        /// Perform cleanup after test runs using assertion on current test name.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            // TODO
        }
        /// <summary>
        /// Perform any initialize for the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }
    }
}
