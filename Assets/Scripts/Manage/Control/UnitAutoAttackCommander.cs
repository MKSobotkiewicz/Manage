using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Manage.Units;
using Manage.Player;

namespace Manage.Control
{
    public class UnitAutoAttackCommander : MonoBehaviour
    {
        public Player.Player Player;

        private float time;
        private float manouverTime;
        private float timeToSpreadout;

        private float RoutDistance = 30;

        private UnitManouvers unitManouvers = new UnitManouvers();

        private System.Random random = new System.Random();

        public void Start()
        {
            time = 0;
            manouverTime = (float)random.NextDouble()*10+10;
            timeToSpreadout = 0;
        }

        public void Update()
        {
            time += Time.fixedDeltaTime;
            if (time >= 1)
            {
                OneSecondUpdate();
                time = 0;
            }
            manouverTime -= Time.fixedDeltaTime;
            timeToSpreadout -= Time.fixedDeltaTime;
        }

        public void OneSecondUpdate()
        {
            foreach (var unit in AllUnitsList.Units)
            {
                Check(unit);
            }
        }

        private void Manouver(Dictionary<float, Unit> ourUnitsClose,Unit target)
        {

            if (timeToSpreadout <= 0)
            {
                timeToSpreadout= (float)random.Next(0, 20)  + 10;
                unitManouvers.Clear();
                foreach (var unit in ourUnitsClose)
                {
                    unitManouvers.Add(unit.Value);
                }
                unitManouvers.SpreadOut(10);
                return;
            }
            if (manouverTime <= 0)
            {
                manouverTime = (float)random.Next(0,20) + 10;
                unitManouvers.Clear();
                var unitToMoveCount = random.Next(ourUnitsClose.Count / 3, ourUnitsClose.Count/2);
                for (int i=0;i< unitToMoveCount; i++)
                {
                    unitManouvers.Add(ourUnitsClose[ourUnitsClose.Keys.Min()]);
                    ourUnitsClose.Remove(ourUnitsClose.Keys.Min());
                }
                unitManouvers.Flank(target);
            }
        }

        private void Check(Unit unit)
        {
            if (unit.Dead || unit.IsMoving())
            {
                return;
            }
            Dictionary<float,Unit> enemyUnitsInRange = new Dictionary<float,Unit>();
            Dictionary<float, Unit> ourUnitsClose = new Dictionary<float, Unit>();
            foreach (var otherUnit in AllUnitsList.Units)
            {
                if (!otherUnit.Dead)
                {
                    if (!Equals(otherUnit.Character.Organization, unit.Character.Organization))
                    {
                        if (unit.IsInRange(otherUnit)&&
                            !enemyUnitsInRange.ContainsKey(Vector3.Distance(otherUnit.Position(), unit.Position())))
                        {
                            enemyUnitsInRange.Add(Vector3.Distance(otherUnit.Position(), unit.Position()), otherUnit);
                        }
                    }
                    else
                    {
                        var distance = Vector3.Distance(otherUnit.Position(), unit.Position());
                        if (distance<100)
                        {
                            if (!ourUnitsClose.ContainsKey(distance))
                            {
                                ourUnitsClose.Add(distance, otherUnit);
                            }
                        }
                    }
                }
            }
            var totalEnemyUnitsInRange = enemyUnitsInRange.Count;
            while (enemyUnitsInRange.Count > 0)
            {
                var targetUnit = enemyUnitsInRange[enemyUnitsInRange.Keys.Min()];
                enemyUnitsInRange.Remove(enemyUnitsInRange.Keys.Min());

                if (CheckVisibilityOfTarget(unit, targetUnit))
                {
                    if (unit.CanPenetrate(targetUnit))
                    {
                        unit.Attack(targetUnit);

                        if (!Equals(unit.Character.Organization, Player.Organization) &&
                            ourUnitsClose.Count > 0)
                        {
                            Manouver(ourUnitsClose, targetUnit);
                            foreach (var ourUnitClose in ourUnitsClose)
                            {
                                if (!ourUnitClose.Value.Attacking )
                                {
                                    ourUnitClose.Value.Attack(targetUnit);
                                    continue;
                                }
                            }
                        }
                        enemyUnitsInRange.Clear();
                        continue;
                    }
                    if (Vector3.Distance(targetUnit.Position(), unit.Position()) < RoutDistance)
                    {
                        unit.Move(Vector3.LerpUnclamped(targetUnit.Position(), unit.Position(), 1.8f));
                    }
                    unit.AttackWithGrenade(targetUnit);
                    continue;
                }
                if (!(unit.Attacking|| unit.Shot||unit.Reloading|| unit.ThrowingGrenade))
                { 
                    unit.Attack(null);
                    if (!Equals(unit.Character.Organization, Player.Organization))
                    {
                        unit.Move(Vector3.Lerp(targetUnit.Position(), unit.Position(), 0.8f));
                    }
                }
            }

            if (!unit.Attacking && unit.AttackedBy.Count > 0)
            {
                var minDistance = 9999f;
                var unitToAttack = unit.AttackedBy.Last().Key;
                foreach (var attackedByUnit in unit.AttackedBy)
                {
                    if (attackedByUnit.Value < minDistance)
                    {
                        minDistance = attackedByUnit.Value;
                        unitToAttack = attackedByUnit.Key;
                    }
                }
                unit.Attack(unitToAttack);
                return;
            }
        }

        private bool CheckVisibilityOfTarget(Unit unit, Unit target)
        {
            if (Equals(target, null))
            {
                UnityEngine.Debug.Log("Not Visible");
                return false;
            }
            var hit=new RaycastHit();
            Vector3 unitPos = unit.transform.position + new Vector3(0, 2f, 0);
            Vector3 targetPos = target.transform.position + new Vector3(0, 2f, 0);
            if (unit.Inventory.Vehicle != null)
            {
                unitPos += new Vector3(0, 2f, 0);
                unitPos = Vector3.Lerp(unitPos, targetPos, 0.1f);
            }
            if (target.Inventory.Vehicle != null)
            {
                targetPos += new Vector3(0, 2f, 0);
                targetPos = Vector3.Lerp(targetPos, unitPos, 0.1f);
            }

            /*UnityEngine.Debug.DrawLine(unitPos,
                                      targetPos, 
                                      Color.blue,
                                      10);*/

            if (!Physics.Linecast(unitPos,
                                  targetPos,
                                  out hit))
            {
                UnityEngine.Debug.Log("Visible");
                return true;
            }
            UnityEngine.Debug.Log("Not Visible");
            return false;
        }
    }
}