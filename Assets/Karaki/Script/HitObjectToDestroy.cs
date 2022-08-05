using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Karaki
{
    public class HitObjectToDestroy : MonoBehaviour
    {
        [SerializeField, Tooltip("接触すると消えるオブジェクトのタグ名")]
        string _tagNameObject = "Bullet";

        [SerializeField, Tooltip("この回数オブジェクトが接触すると消させる")]
        int _hitCount = 1;

        /// <summary>true : 自分のシステムで作られたもの</summary>
        bool _isMyCreated = false;

        /// <summary>true : 自分のシステムで作られたもの</summary>
        public bool IsMyCreated { set => _isMyCreated = value; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_tagNameObject))
            {
                OnHit();
            }
        }

        void OnHit()
        {
            if (_isMyCreated)
            {
                _hitCount--;
                if (_hitCount < 1)
                {
                    PhotonNetwork.Destroy(this.gameObject);
                }
            }
        }
    }
}