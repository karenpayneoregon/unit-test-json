using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerLibrary.Classes
{
    public class PlayerGrouped
    {
        public string Name { get; }
        public List<Player> List { get; }

        public PlayerGrouped(string name, List<Player> list)
        {
            Name = name;
            List = list;
        }

        public override string ToString() => Name;

    }
}
