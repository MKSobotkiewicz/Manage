using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Manage.Units;

namespace Manage.Skills
{
    class ArtilleryStrike: Skill
    {
        private ExplosiveShellType shell = BulletTypes.ShellType_155mm;

        private GameObject FireSoundsPrefab;

        private readonly int shellCount = 3;
        private readonly float attackRadius = 10;
        private readonly float timeBetweenShots = 3;
        private readonly float timeBetweenSounds = 1;

        private bool firing = false;
        private float shellTimer = 3;
        private float soundTimer = 1;
        private int shellsToFire = 0;
        private int shellsToSound = 0;
        private Vector3 firePosition;

        public void Awake()
        {
            Icon = UnityEngine.Resources.Load("UI/SkillsIcons/ArtilleryStrike") as Texture2D;
            ProjectorPrefab = UnityEngine.Resources.Load("UI/AreaEffects/ArtilleryStrikeAreaEffect") as GameObject;
            FireSoundsPrefab = UnityEngine.Resources.Load("UI/AreaEffects/FireSoundsPrefab") as GameObject;
            reloadTime = 60;
        }

        public new void Update()
        {
            ((Skill)this).Update();
            if (firing is false)
            {
                return;
            }
            if (shellsToFire <= 0)
            {
                firing = false;
                return;
            }
            soundTimer -= Time.deltaTime;
            if (soundTimer <= 0&& shellsToSound>0)
            {
                Sound();
                soundTimer = timeBetweenSounds;
                shellsToSound--;
            }
            shellTimer -= Time.deltaTime;
            if (shellTimer <= 0)
            {
                Fire();
                shellTimer = timeBetweenShots;
                shellsToFire --;
            }
        }

        public void Use(Vector3 position)
        {
            firing = true;
            firePosition = position;
            shellsToFire = shellCount;
            shellsToSound = shellCount;
            shellTimer = timeBetweenShots;
            soundTimer = 0;
        }

        public void Sound()
        {
            var go = Instantiate(FireSoundsPrefab);
            go.transform.position = firePosition + new Vector3(300, 900, -300);
        }

        public void Fire()
        {
            var point = Space.Random.PointInSphere(firePosition, attackRadius);
            var bullet = shell.CreateBullet(null, point + new Vector3(300, 900, -300), point);
            bullet.Player = Player;
        }
    }
}
