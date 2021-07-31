using System.Linq;
using System.Text.Json.Serialization;

namespace ContainerLibrary.Classes
{
    public class Customer
    {
        [JsonPropertyName("CustomerIdentifier")]
        public int Identifier { get; set; }
        public string CompanyName { get; set; }
        public int ContactId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int? CountryIdentifier { get; set; }
        public string Phone { get; set; }
        public int ContactTypeIdentifier { get; set; }
        public ContactType[] ContactType { get; set; }
        #region Shortcuts
        public string ContactTitle => ContactType.FirstOrDefault()?.ContactTitle;
        public string ContactFirstName => ContactType.FirstOrDefault()?.Contacts.FirstOrDefault()?.FirstName;
        public string ContactLastName => ContactType.FirstOrDefault()?.Contacts.FirstOrDefault()?.LastName;
        #endregion

        public override string ToString() => CompanyName;
    }
}