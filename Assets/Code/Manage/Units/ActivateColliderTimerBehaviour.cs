using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    public class ActivateColliderTimerBehaviour : MonoBehaviour
    {
        public float Timer = 4f;

        public void Update()
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                Timer = 9999;
                var colliders = GetComponents<Collider>();
                foreach(var collider in colliders)
                {
                    collider.enabled = true;
                }
            }
        }
    }
}
