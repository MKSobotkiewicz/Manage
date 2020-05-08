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
        private ITalkable _speaker;
        private Canvas _canvas;
        private Unit _playerUnit;

        private GameObject talkCloud;

        private bool isActive = false;

        private readonly static string talkCloudPath="UI/Particles/TalkCloudParticleSystem";
        private readonly static string nonEndableTalkCloudPath = "UI/Particles/NonEndableTalkCloudParticleSystem";
        private readonly static string dialogPrefabPath = "UI/Prefabs/DialogCanvas";
        private readonly static float unitDistance = 2;

        public DialogManager()
        {
        }

        public static DialogManager Create(Dialog dialog, ITalkable speaker,Canvas canvas)
        {
            var dialogManager = speaker.GameObject().AddComponent<DialogManager>();
            dialogManager._dialog = GameObject.Instantiate(dialog).GetComponent<Dialog>();
            dialogManager._speaker = speaker;
            dialogManager._speaker.SetDialogManager(dialogManager);
            dialogManager._canvas = canvas;
            dialogManager._dialog.DialogManager = dialogManager;

            if (dialogManager._dialog.Endable)
            {
                dialogManager.talkCloud = Instantiate(UnityEngine.Resources.Load(talkCloudPath), speaker.Transform()) as GameObject;
            }
            else
            {
                dialogManager.talkCloud = Instantiate(UnityEngine.Resources.Load(nonEndableTalkCloudPath), speaker.Transform()) as GameObject;
            }
            dialogManager.talkCloud.transform.parent = dialogManager._speaker.Transform();

            return dialogManager;
        }

        public void Update()
        {
            talkCloud.transform.position = talkCloud.transform.parent.position+new Vector3(0,3,0);
            if (!isActive)
            {
                return;
            }
            if (_speaker is null|| _playerUnit is null)
            {
                return;
            }
            if (Vector3.Distance(_speaker.Transform().position,_playerUnit.Position())> unitDistance)
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
                sentence.Speaker = _speaker;
            }
            var go = Instantiate(UnityEngine.Resources.Load(dialogPrefabPath), _canvas.transform) as GameObject;
            var dialog = go.GetComponent<UI.Dialog>();
            dialog.transform.SetParent(_canvas.transform);
            dialog.ThisDialog = _dialog;
            dialog.MainTalkable = _playerUnit;
            dialog.Begin();
        }

        public void Delete()
        {
            Destroy(talkCloud);
            Destroy(this);
        }
    }
}