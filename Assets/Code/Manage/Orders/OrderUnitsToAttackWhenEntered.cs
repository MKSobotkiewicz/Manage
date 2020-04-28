using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Manage.Units;

namespace Manage.Orders
{
    class OrderUnitsToAttackWhenEntered : Order
    {
        public Player.Player PlayerToTrigger;
        public GameObject PositionToMoveTo;

        private BoxCollider boxCollider;

        public void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        public void OnTriggerEnter(Collider collider)
        {
            var enteringUnit = collider.GetComponentInParent<Unit>();
            if (enteringUnit is null)
            {
                return;
            }
            if (Equals(enteringUnit.Character.Organization, PlayerToTrigger.Organization))
            {
                foreach (var unit in Units)
                {
                    unit.Move(PositionToMoveTo.transform.position);
                }
            }
        }
    }
}
