using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
/*
 * The following does not make sense
 * https://docs.microsoft.com/en-us/answers/questions/812182/need-help-about-listlttgt.html
 */
namespace BaseUnitTestProject1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }

    public class Cart
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class DataOperations
    {
        private static string _FileName = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "carts.json");

        public static void CreateCart()
        {
            List<Cart> cartList = new List<Cart>();

            Cart cart1 = new()
            {
                Quantity = 1,
                Product = new Product
                {
                    Id = 9,
                    Title = "Chai tea",
                    Price = 18m
                }
            };


            Cart cart2 = new()
            {
                Quantity = 1,
                Product = new Product
                {
                    Id = 24,
                    Title = "Irish tea",
                    Price = 4m
                }
            };

            cartList.AddRange(new[] { cart1, cart2 });

            var json = JsonConvert.SerializeObject(cartList, Formatting.Indented);
            File.WriteAllText(_FileName, json);
        }

        public static List<Cart> GetCarts() => 
            JsonConvert.DeserializeObject<List<Cart>>(File.ReadAllText(_FileName));
    }
}
