using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Kawasaki
{
    /// <summary>
    /// マップ
    /// </summary>
    public class Map : MonoBehaviour, IPlayerHit
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; private set; } = 0;

        /// <summary>
        /// マップを増やすことができる
        /// </summary>
        public bool CanAddMap { get; set; } = true;

        public PhotonView View { get; private set; } = null;

        private void Awake()
        {
            View = GetComponent<PhotonView>();
        }

        /// <summary>
        /// セットアップと同期を行う
        /// </summary>
        /// <param name="id">ID</param>
        public void SetUpAndSynchronize(int id)
        {
            SetUp(id);

            object[] parameters = { id };
            View.RPC(nameof(SetUp), RpcTarget.OthersBuffered, parameters);
        }

        /// <summary>
        /// セットアップを行う
        /// </summary>
        /// <param name="id">ID</param>
        [PunRPC]
        public void SetUp(int id)
        {
            Id = id;

            Transform parent = MapsManager.Current.MapsGrid.transform;
            transform.SetParent(parent);
        }

        
        /// <summary>
        /// マップを増やせるかどうか設定する
        /// </summary>
        /// <param name="value"></param>
        public void SetWhetherMapCanBeAdded(bool value)
        {
            CanAddMap = value;

            object[] parameters = { CanAddMap };
            View.RPC(nameof(SynchronizeWhetherMapCanBeAdded), RpcTarget.Others, parameters);
        }

        /// <summary>
        /// マップを増やせるかどうかの設定を同期する
        /// </summary>
        /// <param name="value"></param>
        [PunRPC]
        public void SynchronizeWhetherMapCanBeAdded(bool value)
        {
            CanAddMap = value;
        }

        public void OnHit(Player player)
        {
            MapsManager.Current.UpdateMaps(player, this);
        }
    }
}
