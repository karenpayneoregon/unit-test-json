namespace ConfigurationHelper
{
    /// <summary>
    /// Container for parts of a SQL-Server connection string
    /// </summary>
    public class ApplicationSettings
    {
        public string DatabaseServer { get; set; }
        public string Catalog { get; set; }
        public bool IntegratedSecurity { get; set; }
        public bool UsingLogging { get; set; }
    }

}
