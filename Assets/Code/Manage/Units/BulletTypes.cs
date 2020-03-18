using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Units
{
    public static class BulletTypes
    {
        public static readonly BulletType BulletType_9x19mm = new BulletType("Bullets/9x19mm_Bullet", "Bullets/9x19mm_MuzzleFlash", 400, 0.012, 30, 0);
        public static readonly BulletType BulletType_7p62x39mm = new BulletType("Bullets/7p62x39mm_Bullet", "Bullets/7p62x39mm_MuzzleFlash", 850,0.004,50,10);
        public static readonly BulletType BulletType_5p56x45mm = new BulletType("Bullets/5p56x45mm_Bullet", "Bullets/5p56x45mm_MuzzleFlash", 950, 0.004, 40, 0);
        public static readonly BulletType ShellType_85mm = new BulletType("Bullets/85mm_Shell", "Bullets/85mm_MuzzleFlash", 800, 9.2, 500, 100);
        public static readonly BulletType BulletType_13mm = new BulletType("Bullets/8mm_Bullet", "Bullets/8mm_MuzzleFlash", 850, 0.012, 60, 10);
    }
}
