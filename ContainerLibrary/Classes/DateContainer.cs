using System;

namespace ContainerLibrary.Classes
{
    public class DateContainer
    {
        public DateTime BiosReleaseDate { get; set; }
        public DateTime OsLocalDateTime { get; set; }
        public DateTime OsLastBootUpTime { get; set; }
        public Osuptime OsUptime { get; set; }
    }
}