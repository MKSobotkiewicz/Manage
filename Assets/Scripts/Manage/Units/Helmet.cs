using UnityEngine;

namespace Manage.Units
{
    public class Helmet : MonoBehaviour
    {
        public HelmetType HelmetType { get; private set; }

        public static Helmet Create(HelmetType helmetType, Transform transform)
        {
            if (helmetType == null)
            {
                return null;
            }
            var wgo = Instantiate((UnityEngine.Resources.Load(helmetType.MalePrefabPath) as GameObject), transform);
            var helmet = wgo.GetComponent<Helmet>();
            helmet.HelmetType = helmetType;
            return helmet;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
