using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Manage.Units;
using Manage.Player;

namespace Manage.Control
{
    public class UnitManouvers: HashSet<Unit>
    {
        private System.Random random = new System.Random();
        private EWay way = EWay.None;

        public void Flank(Unit target)
        {
            var randomValue = random.NextDouble();
            if (way == EWay.Left)
            {
                randomValue -= 0.3;
            }
            else if (way == EWay.Right)
            {
                randomValue += 0.3;
            }
            if (randomValue > 0.5)
            {
                randomValue= random.NextDouble();
                if (randomValue > 0.5)
                {
                    FlankRight(target);
                    return;
                }
                WideFlankRight(target);
                return;
            }
            randomValue = random.NextDouble();
            if (randomValue > 0.5)
            {
                FlankLeft(target);
                return;
            }
            WideFlankLeft(target);
        }

        public void SpreadOut(float distance)
        {
            UnityEngine.Debug.Log("Spread Out");
            way = EWay.None;
            var AverageVector = new Vector3();
            foreach (var unit in this)
            {
                AverageVector+= unit.Position();
            }
            AverageVector /= this.Count();
            foreach (var unit in this)
            {
                var distanceVector = unit.Position() - AverageVector;
                UnityEngine.Debug.DrawLine(unit.Position(), unit.Position() + distanceVector, Color.blue, 1f);
                var moveVector = (-distanceVector).normalized * distance;
                UnityEngine.Debug.DrawLine(unit.Position(), unit.Position() + moveVector, Color.yellow, 1f);
                var targetVector = unit.Position() - moveVector;
                UnityEngine.Debug.DrawLine(new Vector3(0, 0, 0), targetVector, Color.magenta, 1f);
                unit.Move(targetVector);
            }
        }

        public void FlankRight(Unit target)
        {
            UnityEngine.Debug.Log("Flank Right");
            way = EWay.Right;
            foreach (var unit in this)
            {
                var distanceVector = unit.Position() - target.Position();
                UnityEngine.Debug.DrawLine(target.Position(), target.Position()+ distanceVector,Color.blue,1f);
                var rotatedDistanceVector = (Vector3.Cross(distanceVector,new Vector3(0, 1, 0))+ distanceVector)/2;
                UnityEngine.Debug.DrawLine(target.Position(), target.Position() + rotatedDistanceVector, Color.yellow, 1f);
                var targetVector = target.Position() + rotatedDistanceVector;
                UnityEngine.Debug.DrawLine(new Vector3(0, 0, 0), targetVector, Color.magenta, 1f);
                unit.Move(targetVector);
            }
        }

        public void FlankLeft(Unit target)
        {
            UnityEngine.Debug.Log("Flank Left");
            way = EWay.Left;
            foreach (var unit in this)
            {
                var distanceVector = unit.Position() - target.Position();
                UnityEngine.Debug.DrawLine(target.Position(), target.Position() + distanceVector, Color.blue, 1f);
                var rotatedDistanceVector = (Vector3.Cross(distanceVector, new Vector3(0, -1, 0))+ distanceVector)/ 2;
                UnityEngine.Debug.DrawLine(target.Position(), target.Position() + rotatedDistanceVector, Color.yellow, 1f);
                var targetVector = target.Position() + rotatedDistanceVector;
                UnityEngine.Debug.DrawLine(new Vector3(0, 0, 0), targetVector, Color.magenta, 1f);
                unit.Move(targetVector);
            }
        }

        public void WideFlankRight(Unit target)
        {
            UnityEngine.Debug.Log("Wide Flank Right");
            way = EWay.Right;
            foreach (var unit in this)
            {
                var distanceVector = unit.Position() - target.Position();
                UnityEngine.Debug.DrawLine(target.Position(), target.Position() + distanceVector, Color.blue, 1f);
                var rotatedDistanceVector = Vector3.Cross(distanceVector, new Vector3(0, 1, 0));
                UnityEngine.Debug.DrawLine(target.Position(), target.Position() + rotatedDistanceVector, Color.yellow, 1f);
                var targetVector = target.Position() + rotatedDistanceVector;
                UnityEngine.Debug.DrawLine(new Vector3(0, 0, 0), targetVector, Color.magenta, 1f);
                unit.Move(targetVector);
            }
        }
        public void WideFlankLeft(Unit target)
        {
            UnityEngine.Debug.Log("Wide Flank Left");
            way = EWay.Left;
            foreach (var unit in this)
            {
                var distanceVector = unit.Position() - target.Position();
                UnityEngine.Debug.DrawLine(target.Position(), target.Position() + distanceVector, Color.blue, 1f);
                var rotatedDistanceVector = Vector3.Cross(distanceVector, new Vector3(0, -1, 0));
                UnityEngine.Debug.DrawLine(target.Position(), target.Position() + rotatedDistanceVector, Color.yellow, 1f);
                var targetVector = target.Position() + rotatedDistanceVector;
                UnityEngine.Debug.DrawLine(new Vector3(0, 0, 0), targetVector, Color.magenta, 1f);
                unit.Move(targetVector);
            }
        }


        private enum EWay
        {
            None,
            Left,
            Right
        }
    }
}
