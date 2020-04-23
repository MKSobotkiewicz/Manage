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
    public class DialogOption : MonoBehaviour
    {
        [TextArea(15, 20)]
        public string Text;
        public Sentence NextSentence;
        public Action Action;

        public new string ToString()
        {
            if (!(NextSentence is null))
            {
                return "<color=#ccffccff>" + Text + "</color>";
            }
            return "<color=#88ff88ff>" + Text + "[END CONVERSATION]</color>";
        }

        public string ToString(Character character)
        {
            if (character is null)
            {
                return Text;
            }
            var speakerName = character.Nickname == "" ?
                character.FirstName + " " + character.Surname :
                character.Nickname;
            return "<color=#ccffccff>[" + speakerName + "]:</color> " + Text ;
        }
    }
}
