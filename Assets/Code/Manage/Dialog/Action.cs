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
    public abstract class Action : MonoBehaviour
    {
        public virtual void Do()
        {
            UnityEngine.Debug.Log("WRONG");
        }
    }
}
