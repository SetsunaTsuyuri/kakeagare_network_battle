using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// 弾を管理するスクリプト
/// </summary>

namespace Tsuguhiko
{
    public class Bullet : MonoBehaviour
    {
     #region private SerializeField

        /// <summary> 弾の速さ</summary>
        [SerializeField, Header("弾の速度")] float _bulletSpeed;



     #endregion

     #region private

        PhotonView _viewBullet;

        Rigidbody2D _rb2DB => GetComponent<Rigidbody2D>();

        /// <summary> 弾の制限</summary>
        int _bulletLimit;

     #endregion

        void Start()
        {
            _viewBullet = GetComponent<PhotonView>();
        }


        void FixedUpdate()
        {
            _rb2DB.velocity = gameObject.transform.rotation * new Vector2(_bulletSpeed, 0);
        }

        void OnBecameInvisible()
        {
            gameObject.SetActive(false);

        }
    }
}
