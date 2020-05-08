using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Manage.Dialog;

namespace Manage.Units
{
    public interface ITalkable
    {
        string GetName();
        string GetFullName();
        GameObject GameObject();
        Transform Transform();
        Texture2D GetPortrait();
        void SetDialogManager(DialogManager dialogManager);
    }
}
