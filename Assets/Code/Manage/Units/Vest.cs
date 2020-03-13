using UnityEngine;

namespace Manage.Units
{
    public class Vest : MonoBehaviour
    {
        public VestType VestType { get; private set; }

        public static Vest Create(VestType vestType, Transform transform, Characters.EGender gender)
        {
            if (vestType == null)
            {
                return null;
            }
            var go = VestType.Load(vestType, transform, gender);
            var vest = go.GetComponent<Vest>();
            vest.VestType = vestType;
            return vest;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
