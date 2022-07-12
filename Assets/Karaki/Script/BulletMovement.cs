using Photon.Pun;
using UnityEngine;

namespace Karaki
{
    /// <summary>弾を管理するスクリプト</summary>
    public class BulletMovement : MonoBehaviourPunCallbacks
    {
        #region メンバ

        [SerializeField, Tooltip("弾の速度")]
        float _bulletSpeed = 5f;

        [SerializeField, Tooltip("弾がどこにも触れずに飛行したとして、自動消滅するまでの時間(s)")]
        float _lifeTime = 5f;

        /// <summary>上記自動消滅処理をするためのタイマー</summary>
        float _timer = 0;

        /// <summary>発射弾の情報を連携させるためのコンポーネント</summary>
        PhotonView _viewBullet = null;

        #endregion

        void Start()
        {
            _viewBullet = GetComponent<PhotonView>();
        }


        void Update()
        {
            //弾の正面方向に向かって移動させる
            transform.position += transform.up * _bulletSpeed * Time.deltaTime;

            //自動消滅処理
            _timer += Time.deltaTime;
            if (_timer > _lifeTime)
            {
                // ネットワークオブジェクトとして Destroy する（他のクライアントからも消える）
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // ネットワークオブジェクトとして Destroy する（他のクライアントからも消える）
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
