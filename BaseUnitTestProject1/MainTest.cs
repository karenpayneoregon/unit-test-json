using System;
using System.Linq;
using BaseUnitTestProject1.Base;
using BaseUnitTestProject1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseUnitTestProject1
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void TestMethod1()
        {
            

            DataOperations.CreateCart();

            var dataCart = DataOperations.GetCarts();

            Console.WriteLine($"Count: {dataCart.Count}");

            foreach (var cart in dataCart)
            {
                Console.WriteLine($"{cart.Product.Id,-3}{cart.Product.Title,-15}{cart.Product.Price:C}");
            }

            Console.WriteLine($"Total: {dataCart.Sum(x => x.Product.Price):C}");
            
        }
        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void TestMethod2()
        {
            // TODO
        }
    }
}
