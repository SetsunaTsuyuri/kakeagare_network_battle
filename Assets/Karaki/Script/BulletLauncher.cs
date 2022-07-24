using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Karaki
{
    /// <summary>弾を撃つ機能</summary>
    public class BulletLauncher : MonoBehaviour
    {
        #region メンバ
        [SerializeField, Tooltip("使用する弾のプレハブ名")]
        string _bulletPrefab = "Bullet";

        [SerializeField, Tooltip("弾を射出する場所")]
        Transform _muzzle = null;

        /// <summary>発射弾の情報を連携させるためのコンポーネント</summary>
        PhotonView _viewBullet = null;
        #endregion

        void Start()
        {
            _viewBullet = GetComponent<PhotonView>();
        }

        public void Fire()
        {
            //このコンポーネントが自分のキャラのものなら弾を発射
            if (_viewBullet.IsMine)
            {
                if (_bulletPrefab != null && _bulletPrefab.Length > 0)
                {
                    // 自分の弾をネットワークオブジェクトとして生成する
                    GameObject bullet = PhotonNetwork.Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
                    bullet.GetComponent<BulletMovement>().IsMine = true;
                }
            }
        }
    }
}
