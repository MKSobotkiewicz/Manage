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
            Destroy(gameObject);
        }
    }
}
