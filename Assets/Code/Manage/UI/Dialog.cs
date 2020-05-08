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
    public class Dialog : MonoBehaviour
    {
        public RawImage Portrait1;
        public RawImage Portrait2;
        public Text Name1;
        public Text Name2;
        public Text DialogTextbox;
        public Button CloseButton;
        public List<Text> DialogOptions;
        public List<Image> DialogOptionsSelectPanel;
        public Scrollbar DialogTextBoxScrollbar;
        public AudioSource ClickingSource;

        public Manage.Dialog.Dialog ThisDialog;
        public ITalkable MainTalkable;

        private string currentText;
        private string targetText;
        private float timer;

        private static bool isAnyDialogOpen=false;

        public void Begin()
        {
            if (isAnyDialogOpen is true)
            {
                Delete();
                return;
            }
            isAnyDialogOpen = true;
            ClickingSource.mute = true;
            currentText = "";
            targetText = "";
            timer = 0;
            if (ThisDialog.Endable)
            {
                CloseButton.gameObject.SetActive(false);
            }
            foreach (var dialogOptionsSelectPanel in DialogOptionsSelectPanel)
            {
                dialogOptionsSelectPanel.enabled = false;
            }
            DialogTextbox.text = "";
            foreach (var dialogOption in DialogOptions)
            {
                dialogOption.text = "";
                dialogOption.gameObject.SetActive(false);
            }
            Portrait2.texture = MainTalkable.GetPortrait();
            Name2.text = MainTalkable.GetFullName();
            Click();
        }

        public void Update()
        {
            DialogTextbox.text = "<color=#000000ff>" + currentText+ "</color>";

            timer += Time.deltaTime;
            if (targetText.Length > 0)
            {
                ClickingSource.mute = false;
                if (timer > 0.05)
                {
                    timer = 0;
                    currentText += targetText[0];
                    targetText = targetText.Remove(0, 1);
                    DialogTextBoxScrollbar.value = 0;
                    return;
                }
            }
            ClickingSource.mute = true;
            if (timer > 1)
            {
                timer = 0;
                Click();
            }
        }

        public void Click()
        {
            var nextSentence = ThisDialog.NextSentence();
            if (nextSentence is null)
            {
                currentText += targetText;
                targetText = "";
                UnityEngine.Debug.Log("Write dialog options");
                WriteDialogOptions(ThisDialog.NextDialogOptions);
                return;
            }
            UnityEngine.Debug.Log("Write next sentence");
            WriteSentence(nextSentence);
        }

        public void Delete()
        {
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.1f).setOnComplete(Destroy).setEase(LeanTweenType.easeInCubic);
        }

        public void End()
        {
            isAnyDialogOpen = false;
            ThisDialog.End();
            Delete();
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void WriteDialogOptions(List<Manage.Dialog.DialogOption> dialogOptions)
        {
            for (var i= 0;i< dialogOptions.Count;i++)
            {
                DialogOptions[i].gameObject.SetActive(true);
                DialogOptions[i].text = dialogOptions[i].ToString();
            }
        }

        public void MoveCursorOverDialogOption(int i)
        {
            if (DialogOptions[i].text!="")
            {
                DialogOptionsSelectPanel[i].enabled = true;
            }
        }

        public void MoveCursorAwayFromDialogOption(int i)
        {
            DialogOptionsSelectPanel[i].enabled = false;
        }

        public void ClickOnDialogOption(int i)
        {
            if (DialogOptions[i].text == "")
            {
                return;
            }
            foreach (var dialogOption in DialogOptions)
            {
                dialogOption.text = "";
                dialogOption.gameObject.SetActive(false);
            }
            foreach (var dialogOptionsSelectPanel in DialogOptionsSelectPanel)
            {
                dialogOptionsSelectPanel.enabled = false;
            }
            UnityEngine.Debug.Log(ThisDialog.NextDialogOptions[i].ToString(MainTalkable));
            if (ThisDialog.NextDialogOptions[i].Action != null)
            {
                ThisDialog.NextDialogOptions[i].Action.Do();
            }
            AddTextToDialogTextbox(ThisDialog.NextDialogOptions[i].ToString(MainTalkable));
            if (!ThisDialog.SetNextSentence(ThisDialog.NextDialogOptions[i]))
            {
                End();
            }
        }

        private void WriteSentence(Manage.Dialog.Sentence sentence)
        {
            Portrait1.texture = sentence.Speaker.GetPortrait();
            Name1.text = sentence.Speaker.GetFullName();
            AddTextToDialogTextbox(sentence.ToString());
        }

        private void AddTextToDialogTextbox(string text)
        {
            currentText += targetText;
            targetText = text + Environment.NewLine;
            while (targetText[0] != ':')
            {
                currentText += targetText[0];
                targetText = targetText.Remove(0, 1);
            }
            for (int i=0;i<9;i++)
            {
                currentText += targetText[0];
                targetText = targetText.Remove(0, 1);
            }
            DialogTextBoxScrollbar.value = 0;
        }
    }
}