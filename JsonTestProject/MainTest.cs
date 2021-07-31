using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationHelper;
using ContainerLibrary.Classes;
using Json.Library;
using Json.Library.Extensions;
using JsonTestProject.Base;
using JsonTestProject.Classes;
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

            var singleCustomer = customers.FirstOrDefault(customer => customer.CustomerIdentifier == 2);


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
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
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

        [TestMethod]
        [TestTraits(Trait.AppSettings)]
        public void ConnectionStringTest()
        {
            var result = Helper.ConnectionString();
            Assert.IsTrue(result == NorthWindConnectionString);
        }


    }

}

