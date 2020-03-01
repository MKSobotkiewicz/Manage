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
    public class DialogManager:MonoBehaviour
    {
        private Dialog _dialog;
        private Unit _unit;
        private Canvas _canvas;
        private Unit _playerUnit;

        private bool isActive = false;

        private static string talkCloudPath="UI/Particles/TalkCloudParticleSystem";
        private static string dialogPrefabPath = "UI/Prefabs/DialogCanvas";
        private static float unitDistance = 2;

        public DialogManager()
        {
        }

        public static DialogManager Create(Dialog dialog, Unit unit,Canvas canvas)
        {
            var dialogManager = unit.gameObject.AddComponent<DialogManager>();
            dialogManager._dialog = GameObject.Instantiate(dialog).GetComponent<Dialog>();
            dialogManager._unit = unit;
            dialogManager._unit.DialogManager = dialogManager;
            dialogManager._canvas = canvas;

            var tc = Instantiate(UnityEngine.Resources.Load(talkCloudPath), unit.transform) as GameObject;
            tc.transform.parent = dialogManager._unit.transform;

            return dialogManager;
        }

        public void Update()
        {
            if (!isActive)
            {
                return;
            }
            if (_unit is null|| _playerUnit is null)
            {
                return;
            }
            if (Vector3.Distance(_unit.Position(),_playerUnit.Position())> unitDistance)
            {
                return;
            }
            isActive = false;
            BeginDialog();
        }

        public void PoolDialog(Unit playerUnit)
        {
            _playerUnit = playerUnit;
            isActive = true;
        }

        public void BeginDialog()
        {
            foreach (var sentence in _dialog.transform.GetComponentsInChildren<Manage.Dialog.Sentence>())
            {
                sentence.Speaker = _unit;
            }
            var go = Instantiate(UnityEngine.Resources.Load(dialogPrefabPath), _canvas.transform) as GameObject;
            var dialog = go.GetComponent<UI.Dialog>();
            dialog.transform.SetParent(_canvas.transform);
            dialog.ThisDialog = _dialog;
            dialog.MainCharacter = _playerUnit;
            dialog.Begin();
        }
    }
}