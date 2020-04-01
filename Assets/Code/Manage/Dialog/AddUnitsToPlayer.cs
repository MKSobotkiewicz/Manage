using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Manage.Characters;
using Manage.Units;

namespace Manage.Dialog
{
    public class AddUnitsToPlayer :Action
    {
        public Player.Player Player;
        public UI.UnitIds UnitIds;
        public List<Unit> Units;

        public override void Do()
        {
            foreach (var unit in Units)
            {
                unit.Character.Organization = Player.Organization;
                Player.Units.Add(unit);
            }
            UnitIds.Reset();
        }
    }
}
