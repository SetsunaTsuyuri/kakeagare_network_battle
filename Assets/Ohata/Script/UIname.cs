using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Kawasaki;

namespace Ohata
{
    public class UIname : MonoBehaviour
    {
        [SerializeField, Header("playerの名前")]
        TextMeshProUGUI _text;

        Transform _target;

        [SerializeField, Header("テキスト")]
        PlayerUI _playerUi;

        PhotonView _viewName;
        void Start()
        {
            if(_playerUi == PlayerUI.You)
            {
                Player myPlayer = PlayersManager.Current.GetMyPlayer();
                _text.text = "You";
                _target = myPlayer.transform;
            }
            else if(_playerUi == PlayerUI.Enemy)
            {
                _text.text = "Enemy";
                Player enemy = PlayersManager.Current.GetAnotherPlayer();
                _target = enemy.transform;

            }
        }
    }
}
