using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Karaki
{
    public class HitObjectToDestroy : MonoBehaviour
    {
        [SerializeField, Tooltip("倒されて消えるときに発生させるエフェクトプレハブ")]
        GameObject _PrefDefeatEffect = null;

        [SerializeField, Tooltip("接触するとダメージを受けるオブジェクトのタグ名")]
        string _tagNameDamageObject = "Bullet";

        [SerializeField, Tooltip("接触すると即倒されるオブジェクトのタグ名")]
        string _tagNameDefeatObject = "Player";

        [SerializeField, Tooltip("この回数オブジェクトが接触すると消させる")]
        int _hitCount = 1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_tagNameDamageObject))
            {
                OnHit();
            }
            else if (collision.gameObject.CompareTag(_tagNameDefeatObject))
            {
                OnHit(int.MaxValue);
            }
        }

        void OnHit(int Damage = 1)
        {
            //マスタークライアントのみ、ダメージの処理
            if (PhotonNetwork.IsMasterClient)
            {
                _hitCount -= Damage;
                if (_hitCount < 1)
                {
                    PhotonNetwork.Destroy(this.gameObject);
                }
            }
        }

        void OnDestroy()
        {
            //倒された時のエフェクトは同期させる必要性がないため、個別に実行
            GameObject effect = Instantiate(_PrefDefeatEffect);
            effect.transform.position = transform.position;
        }
    }
}