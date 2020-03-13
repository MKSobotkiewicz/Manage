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
    public class Sentence : MonoBehaviour
    {
        public Unit Speaker;
        public string Text;
        public Sentence NextSentence;
        public List<DialogOption> DialogOptions;

        public new string ToString()
        {
            var speakerName = Speaker.Character.Nickname == "" ? 
                Speaker.Character.FirstName + " " + Speaker.Character.Surname : 
                Speaker.Character.Nickname;
            return "<color=#00ff00ff>[" + speakerName + "]:</color> " + Text;
        }

        public bool IsLast()
        {
            if (NextSentence is null)
            {
                return true;
            }
            return false;
        }
    }
}
