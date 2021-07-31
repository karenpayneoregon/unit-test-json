using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonTestProject.Classes;


// ReSharper disable once CheckNamespace - do not change
namespace JsonTestProject
{
    public partial class MainTest
    {
        public string ReadFileName { get; set; }
        public string ContactSerializeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ContactsSerialized.json");
        public string CustomersSerializeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CustomersSerialized.json");
        public string ComputerDetailsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "computerDateInfo.json");

        public static string NorthWindConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=NorthWind2020;Integrated Security=True";

        [TestInitialize]
        public async Task Initialization()
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
