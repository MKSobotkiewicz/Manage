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
        public ITalkable Speaker;
        [TextArea(15, 20)]
        public string Text;
        public Sentence NextSentence;
        public List<DialogOption> DialogOptions;

        public new string ToString()
        {
            var speakerName = Speaker.GetName(); 
            return "<color=#88ff88ff>[" + speakerName + "]:</color> " + Text;
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
