using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Characters
{
    public class Body : Space.IPositioned
    {
        public BodyPart Head { get; private set; }
        public BodyPart Chest { get; private set; }
        public BodyPart Abdomen { get; private set; }
        public BodyPart LeftArm { get; private set; }
        public BodyPart RightArm { get; private set; }
        public BodyPart LeftLeg { get; private set; }
        public BodyPart RightLeg { get; private set; }

        private Vector3 _Position;

        public Body()
        {
            Head = new BodyPart("Head",10);
            Chest = new BodyPart("Chest",15);
            Abdomen = new BodyPart("Abdomen",15);
            LeftArm = new BodyPart("Left Arm",10);
            RightArm = new BodyPart("Right Arm",10);
            LeftLeg = new BodyPart("Left Leg",20);
            RightLeg = new BodyPart("Right Leg",20);
            if (Head.HitChance+
                Chest.HitChance+
                Abdomen.HitChance+
                LeftArm.HitChance+
                RightArm.HitChance+
                LeftLeg.HitChance+
                RightLeg.HitChance!=100)
            {
                throw new Exception();
            }
        }

        public Vector3 Position()
        {
            return _Position;
        }

        public Space.IPositioned Rotate(Vector3 position)
        {
            return this;
        }

        public Space.IPositioned Move(Vector3 position)
        {
            _Position += position;
            return this;
        }

        public class BodyPart
        {
            public string Name { get; private set; }
            public int HitPoints { get; private set; }
            public int HitChance { get; private set; }
            public bool IsDestroyed { get; private set; }

            public BodyPart(string name,int hitChance)
            {
                Name = name;
                HitPoints = 100;
                IsDestroyed = false;
                if (hitChance < 0 || hitChance > 100)
                {
                    throw new Exception();
                }
                HitChance = hitChance;
            }

            public bool Damage(int value)
            {
                if ((HitPoints -= value)<=0)
                {
                    HitPoints = 0;
                    IsDestroyed = true;
                }
                return IsDestroyed;
            }
        }
    }
}
