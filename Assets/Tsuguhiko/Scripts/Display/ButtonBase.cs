using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tsuguhiko
{

    public class ButtonBase : MonoBehaviour
    {
        [SerializeField, Header("各種のボタン")] Button[] buttons;

        public Button[] Buttons { get; set; }

        void Start()
        {
            buttons = Buttons;
        }
    }

}
