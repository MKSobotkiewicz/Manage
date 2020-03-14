using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manage.Audio
{
    public class Footsteps : SoundManager
    {
        private float speed=0;
        private float time=0;

        void Update()
        {
            time += Time.deltaTime;
            if (speed <= 0.01)
            {
                return;
            }
            if (time >= 3/ speed)
            {
                Play();
                time = (float)random.NextDouble()*0.2f-0.1f;
            }
        }

        public void SetSpeed(float _speed)
        {
            speed = _speed;
        }
    }
}
