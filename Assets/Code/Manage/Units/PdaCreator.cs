using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manage.Characters;
using Manage.Organizations;
using Manage.Dialog;

namespace Manage.Units
{
    public class PdaCreator : MonoBehaviour
    {
        public bool CreateOnStart = true;
        public Player.Player Player;
        public Canvas MainCanvas;
        public Pda PdaPrefab;
        public string Name = "Device";
        public Dialog.Dialog Dialog;

        public void Start()
        {
            if (!CreateOnStart)
            {
                return;
            }
            CreateAndDestroy();
        }

        public void CreateAndDestroy()
        {
            var pda = Create();
            if (Dialog != null)
            {
                var dialogManager = DialogManager.Create(Dialog, pda, MainCanvas);
            }
            Destroy(gameObject);
        }

        public Pda Create()
        {
            var pda = Instantiate(PdaPrefab,null);
            pda.Name = Name;
            pda.transform.position = transform.position;
            return pda;
        }
    }
}
