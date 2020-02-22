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
    public class Dialog:MonoBehaviour
    {
        public Sentence FirstSentence;
        public List<DialogOption> NextDialogOptions { get; private set; }
        private Sentence CurrentSentence;

        public void Start()
        {
            CurrentSentence = FirstSentence;
        }

        public bool SetNextSentence(DialogOption dialogOption)
        {
            if ((CurrentSentence = dialogOption.NextSentence) is null)
            {
                return false;
            }
            return true;
        }

        public Sentence NextSentence()
        {
            if (CurrentSentence == null)
            {
                return null;
            }
            var sentenceToReturn = CurrentSentence;

            CurrentSentence = CurrentSentence.NextSentence;
            if (CurrentSentence == null)
            {
                NextDialogOptions = sentenceToReturn.DialogOptions;
            }

            return sentenceToReturn;
        }
    }
}
