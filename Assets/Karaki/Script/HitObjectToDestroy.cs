using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Karaki
{
    public class HitObjectToDestroy : MonoBehaviour
    {
        [SerializeField, Tooltip("接触すると消えるオブジェクトのタグ名")]
        string _tagNameObject = "Bullet";

        [SerializeField, Tooltip("この回数オブジェクトが接触すると消させる")]
        int _hitCount = 1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_tagNameObject))
            {
                OnHit();
            }
        }

        void OnHit()
        {
            _hitCount--;
            if (_hitCount < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}