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
    public class CharacterPortrait : MonoBehaviour
    {
        public void Click()
        {
            var cc=transform.GetComponentInParent<CreateCharacter>();
            if (cc != null)
            {
                cc.SetImage(GetComponent<RawImage>().texture as Texture2D);
            }
        }
    }
}