using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    public class Pda:MonoBehaviour,ITalkable
    {
        public string Name;
        public Texture2D Portrait;
        public Dialog.DialogManager DialogManager;

        public string GetName()
        {
            return Name;
        }

        public string GetFullName()
        {
            return "PDA" + Environment.NewLine + Name;
        }

        public Texture2D GetPortrait()
        {
            return Portrait;
        }

        public GameObject GameObject()
        {
            return gameObject;
        }
        public Transform Transform()
        {
            return transform;
        }

        public void SetDialogManager(Dialog.DialogManager dialogManager)
        {
            DialogManager = dialogManager;
        }
    }
}
