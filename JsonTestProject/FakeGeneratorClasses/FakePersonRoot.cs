using System;
using System.Text.Json.Serialization;

namespace JsonTestProject.FakeGeneratorClasses
{

    public class FakePersonRoot
    {
        public string status { get; set; }
        public int code { get; set; }
        public int total { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string birthday { get; set; }
        public DateTime BirthDate => Convert.ToDateTime(birthday);
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [JsonPropertyName("address")]
        public Address Address { get; set; }
        public string website { get; set; }
        public string image { get; set; }
    }

    public class Address
    {
        public int id { get; set; }
        public string street { get; set; }
        public string streetName { get; set; }
        public string buildingNumber { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
        public string county_code { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

}
