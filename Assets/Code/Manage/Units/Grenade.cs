using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    public class Grenade
    {
        public string Path { get; private set; }
        public int Damage { get; private set; }

        public Grenade(string path, int damage)
        {
            Path = path;
            Damage = damage;
        }

        public GameObject Instantiate(Transform parent,Vector3 position, Quaternion rotation)
        {
            var output= GameObject.Instantiate(UnityEngine.Resources.Load<GameObject>(Path)) as GameObject;
            output.GetComponent<GrenadeBehaviour>().Damage = Damage;
            output.transform.SetParent(parent);
            output.transform.rotation = rotation;
            output.transform.position = position;
            return output;
        }
    }
}
