using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Kawasaki;

namespace Ohata
{

    public class UITarget : MonoBehaviour
    {
        [SerializeField, Header("敵の方向を表示")]
        Transform _tekitarget;
        Transform _target;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _target.LookAt(_tekitarget);
        }
    }
}
