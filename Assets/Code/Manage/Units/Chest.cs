using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Manage.Units
{
    public class Chest : MonoBehaviour
    {
        public List<ItemType> itemTypes = new List<ItemType>();
        private Player.Player player;

        public static List<Chest> AllChests = new List<Chest>();

        private bool isGoingToBePickedUp = false;

        private static readonly System.Random random = new System.Random();

        public void Start()
        {
            AllChests.Add(this);
            transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddRelativeForce(new Vector3(random.Next(-20, 20), random.Next(20, 40), random.Next(-20, 20)), ForceMode.Impulse);
            rigidbody.AddRelativeTorque(new Vector3(random.Next(-40, 40), random.Next(-40, 40), random.Next(-40, 40)), ForceMode.Impulse);
        }
        public void Update()
        {
            if (isGoingToBePickedUp)
            {
                foreach (var unit in player.Units)
                {
                    if (Vector3.Distance(unit.Position(), transform.position) < 2)
                    {
                        PickUp(player);
                    }
                }
            }
        }

        public void SetPlayer(Player.Player _player)
        {
            player = _player;
            isGoingToBePickedUp = true;
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
