using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    public class GrenadeType : ItemType
    {
        public string PrefabPath { get; private set; }
        public int Damage { get; private set; }
        public float Range { get; private set; }
        public float Radius { get; private set; }

        public GrenadeType(string name,
                           string info,
                           string prefabPath,
                           string iconPath,
                           int damage,
                           float range, 
                           float radius) : base(name, 
                                                 info, 
                                                 iconPath)
        {
            PrefabPath = prefabPath;
            Damage = damage;
            Range = range;
            Radius = radius;
        }

        public GameObject Instantiate(Transform parent,Vector3 position, Quaternion rotation)
        {
            var output= GameObject.Instantiate(UnityEngine.Resources.Load<GameObject>(PrefabPath)) as GameObject;
            output.GetComponent<GrenadeBehaviour>().Damage = Damage;
            output.GetComponent<SphereCollider>().radius = Radius;
            output.transform.SetParent(parent);
            output.transform.rotation = rotation;
            output.transform.position = position;
            return output;
        }
    }
}
