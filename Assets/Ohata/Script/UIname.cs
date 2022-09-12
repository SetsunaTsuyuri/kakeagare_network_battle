using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Kawasaki;

namespace Ohata
{
    public class UIName : MonoBehaviour
    {
        [SerializeField, Header("playerの名前")]
        TextMeshProUGUI _text;

        Transform _target;

        [SerializeField, Header("テキスト")]
        PlayerUI _playerUi;

        Camera _camera;

        RectTransform _rectTransform;

        PhotonView _viewName;

        



        void Start()
        {
            _rectTransform = transform as RectTransform;
            _camera = Camera.main;
            _text = GetComponentInChildren<TextMeshProUGUI>();

            
           
        }

        



        private void LateUpdate()
        {
            if(_target == null)
            {
                RegisterPlayer();
            }
            else
            {
                Vector2 position = RectTransformUtility.WorldToScreenPoint(_camera, _target.position);
                _rectTransform.position = position;
            }
           
           

             
        }



        private void RegisterPlayer()
        {
            switch (_playerUi)
            {
                case PlayerUI.You:

                    Player myPlayer = PlayersManager.Current.GetMyPlayer();
                    if(myPlayer != null)
                    {
                        _text.text = "You";
                        _target = myPlayer.transform;

                    }
                   
                    break;
                case PlayerUI.Enemy:
                  
                    Player enemy = PlayersManager.Current.GetAnotherPlayer();
                    if(enemy != null)
                    {
                        _text.text = "Enemy";
                        _target = enemy.transform;
                    }
                   
                    break;
                default:
                    break;
            }
        }
    }
}
