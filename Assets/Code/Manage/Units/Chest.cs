using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Manage.Units
{
    public class Chest:MonoBehaviour
    {
        public List<ItemType> itemTypes=new List<ItemType>();

        public static List<Chest> AllChests=new List<Chest>();

        public void Start()
        {
            AllChests.Add(this);
        }

        public void PickUp(Player.Player player)
        {
            foreach (var itemType in itemTypes)
            {
                player.PlayerInventory.Add(itemType);
            }
            Destroy();
        }

        public void Destroy()
        {
            AllChests.Remove(this);
            Destroy(gameObject);
        }

        public override string ToString()
        {
            var output = "";
            for (var i=0;i< itemTypes.Count;i++)
            {
                output += itemTypes[i].Name;
                if (i != itemTypes.Count-1)
                {
                    output += "\n";
                }
            }
            return output;
        }
    }
}
