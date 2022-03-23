using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerLibrary.Classes
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public override string ToString() => Name;
    }
}
