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
        /// IDを設定して同期する
        /// </summary>
        /// <param name="id">ID</param>
        public void SetAndSynchronizeId(int id)
        {
            Id = id;
            object[] parameters = { Id };
            View.RPC(nameof(SetId), RpcTarget.OthersBuffered, parameters);
        }

        /// <summary>
        /// IDを設定する
        /// </summary>
        /// <param name="id">ID</param>
        [PunRPC]
        public void SetId(int id)
        {
            Id = id;
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
