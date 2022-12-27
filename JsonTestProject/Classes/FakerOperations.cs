using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTestProject.Classes
{
    public enum Gender
    {
        male,
        female
    }
    public class FakerOperations
    {
        public static string CreateMalePeople(int quantity, DateTime birthDate)
        {
            var date = $"{birthDate:yyyy-M-d}";
            return $"persons?_quantity={quantity}&_gender=male&_birthday_start={date}";
        }
        public static string CreateFemalePeople(int quantity, DateTime birthDate)
        {
            var date = $"{birthDate:yyyy-M-d}";
            return $"persons?_quantity={quantity}&_gender=female&_birthday_start={date}";
        }
    }
}
