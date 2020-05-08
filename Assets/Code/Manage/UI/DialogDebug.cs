using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Manage.Characters;
using Manage.Units;

namespace Manage.UI
{
    public class DialogDebug : MonoBehaviour
    {
        public GameObject DialogPrefab;
        public Manage.Dialog.Dialog Dialog;
        public Player.Player Player;

        public void Begin()
        {
            foreach (var sentence in transform.GetComponentsInChildren<Manage.Dialog.Sentence>())
            {
                sentence.Speaker = AllUnitsList.Units.Last();
            }
            var go = Instantiate(DialogPrefab, transform);
            var dialog = go.GetComponent<Dialog>();
            dialog.transform.SetParent(transform.parent);
            dialog.ThisDialog = Dialog;
            dialog.MainTalkable = AllUnitsList.Units.First();
            dialog.Begin();
        }
    }
}
