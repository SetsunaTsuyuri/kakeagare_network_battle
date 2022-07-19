using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
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

        /// <summary>
        /// フォトンビュー
        /// </summary>
        public PhotonView View { get; private set; } = null;

        public void OnHit(Player player)
        {
            // マップを更新する
            MapsManager.Current.UpdateMaps(player, this);
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
        /// セットアップする
        /// </summary>
        /// <param name="id">ID</param>
        [PunRPC]
        public void SetUp(int id)
        {
            // ID
            Id = id;

            // 親トランスフォーム
            Transform parent = MapsManager.Current.MapsGrid.transform;
            transform.SetParent(parent);

            // フォトンビュー
            View = GetComponent<PhotonView>();

            // タイルマップのセルバウンズを圧縮する
            Tilemap[] tilemaps = GetComponentsInChildren<Tilemap>();
            foreach (var map in tilemaps)
            {
                map.CompressBounds();
            }
        }

        
        /// <summary>
        /// マップを増やせるかどうかの設定と同期を行う
        /// </summary>
        /// <param name="value"></param>
        public void SetWhetherMapCanBeAddedAndSynchronize(bool value)
        {
            SetWhetherMapCanBeAdded(value);

            object[] parameters = { CanAddMap };
            View.RPC(nameof(SetWhetherMapCanBeAdded), RpcTarget.Others, parameters);
        }

        /// <summary>
        /// マップを増やせるかどうかを設定する
        /// </summary>
        /// <param name="value"></param>
        [PunRPC]
        public void SetWhetherMapCanBeAdded(bool value)
        {
            CanAddMap = value;
        }
    }
}
