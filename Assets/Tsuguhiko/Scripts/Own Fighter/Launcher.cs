using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using static Photon.Pun.PhotonNetwork;
using System.Collections;

namespace Tsuguhiko
{
    public class Launcher : MonoBehaviour
    {
        #region private SerializeField

        /// <summary> 弾のプレハブ</summary>
        [SerializeField, Header("弾のプレハブスクリプト")] GameObject _bulletPrefab;

        [SerializeField] GameObject _muzzle;
        #endregion

        #region private

        PhotonView _viewLauncher;

        List<Bullet> _bulletPool = new List<Bullet>();

        Transform _poolPos;

        #endregion
          void Start()
          {
             _viewLauncher = GetComponent<PhotonView>();

            _poolPos = new GameObject("Tsugu_Bullet").transform;
          }

        //GameObject IPunPrefabPool.Instantiate(string prefabId, Vector3 pos, Quaternion quate)
        //{
        //    Bullet bullet;

        //    prefabId = "Tsugu_Bullet";

        //    if (_bulletPool.Count > 0)
        //    {
        //        bullet = _bulletPool.Pop();

        //        bullet.transform.SetPositionAndRotation(pos, quate);
        //    }
        //    else
        //    {
        //        bullet = Instantiate(_bulletPrefab,pos,quate);

        //        bullet.gameObject.SetActive(false);

        //        return bullet.gameObject;
        //    }
        //    return null;
        //}
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ObjectPool(_muzzle.transform.position, Quaternion.identity);
            }
        }

        //void IPunPrefabPool.Destroy(GameObject gameObject)
        //{
        //    var bullet = gameObject.GetComponent<Bullet>();

        //    _bulletPool.Push(bullet);
        //}

        void ObjectPool(Vector3 pos , Quaternion quate)
        {
            foreach(Transform t in _poolPos)
            {
                if (!t.gameObject.activeSelf)
                {
                    t.SetPositionAndRotation(pos, quate);
                    t.gameObject.SetActive(true);//位置と回転を設定後、アクティブにする
                    return;
                }
            }

            //非アクティブな弾がないなら生成
            Instantiate(_bulletPrefab, pos, quate, _poolPos);//生成と同時にPlayerBulletを親に設定
            _bulletPrefab.SetActive(true);//弾をアクティブにする
        }
    }
}
