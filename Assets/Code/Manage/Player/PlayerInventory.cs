using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage.Units;

namespace Manage.Player
{
    public class PlayerInventory:List<ItemType>
    {
        public new void Add(ItemType itemType)
        {
            if (itemType!=ArmorTypes.BasicArmor)
            {
                base.Add(itemType);
            }
        }
    }
}
