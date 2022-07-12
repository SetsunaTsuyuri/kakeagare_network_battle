using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Karaki
{
    /// <summary>弾を撃つ機能</summary>
    public class BulletLauncher : MonoBehaviour
    {
        #region メンバ
        [SerializeField, Tooltip("使用する弾のプレハブ")]
        GameObject _bulletPrefab = null;

        [SerializeField, Tooltip("弾を射出する場所")]
        Transform _muzzle = null;

        [SerializeField, Tooltip("入力ボタン名 : 弾を撃つ")]
        string _inputNameShoot = "Fire1";

        /// <summary>発射弾の情報を連携させるためのコンポーネント</summary>
        PhotonView _viewBullet = null;
        #endregion

        void Start()
        {
            _viewBullet = GetComponent<PhotonView>();
        }

        void Update()
        {
            //ボタン押すたびに発射
            if (Input.GetButtonDown(_inputNameShoot))
            {
                if (_bulletPrefab != null)
                {
                    // ネットワークオブジェクトとして生成する
                    PhotonNetwork.Instantiate(_bulletPrefab.name, _muzzle.position, _muzzle.rotation);
                }
            }
        }
    }
}
