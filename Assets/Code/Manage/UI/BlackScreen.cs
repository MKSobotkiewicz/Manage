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
    public class BlackScreen : MonoBehaviour
    {
        public void Close()
        {
            LeanTween.alpha(GetComponent<RectTransform>(), 0, 2f).setOnComplete(Destroy).setEase(LeanTweenType.easeInCubic);
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
