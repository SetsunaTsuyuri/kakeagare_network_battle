using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Kawasaki
{
    /// <summary>
    /// プレイヤーが攻撃した際、自身に命中しないようレイヤーを変更するコンポーネント
    /// </summary>
    public class PlayerAttack : MonoBehaviour
    {
        private void Awake()
        {
            PhotonView photonView = GetComponent<PhotonView>();
            if (photonView && photonView.IsMine)
            {
                int layer = LayerMask.NameToLayer("PlayerAttack");
                Transform[] children = GetComponentsInChildren<Transform>(true);
                foreach (var child in children)
                {
                    child.gameObject.layer = layer;
                }
            }
        }
    }
}
