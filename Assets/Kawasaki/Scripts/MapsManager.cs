using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using Photon.Pun;

namespace Kawasaki
{
    /// <summary>
    /// マップの管理者
    /// </summary>
    public class MapsManager : MonoBehaviour
    {
        /// <summary>マップを構成するプレハブの名前</summary>
        [SerializeField] string _mapPrefabName;
        /// <summary>マップのプレハブをいくらおきに設置するか</summary>
        [SerializeField] float _offsetY = 10f;
        /// <summary>KillZone のプレハブ名</summary>
        [SerializeField] string _killZonePrefabName;
        /// <summary>KillZone の初期位置</summary>
        [SerializeField] Vector3 _killZoneInitialPosition;

        /// <summary>
        /// 現在のシーンに存在するインスタンス
        /// </summary>
        public static MapsManager Current = null;

        /// <summary>
        /// 最初に作るマップの数
        /// </summary>
        [SerializeField]
        int _initialMaps = 3;

        /// <summary>
        /// 削除するマップIDのオフセット
        /// </summary>
        [SerializeField]
        int _idOffsetOfmapToRemove = 2;

        /// <summary>
        /// マップを作った数
        /// </summary>
        public int MapCreationCount { get; private set; } = 0;

        private void Awake()
        {
            Current = this;
        }

        /// <summary>
        /// マップを作る
        /// </summary>
        public void CreateMaps()
        {
            for (int i = 0; i < _initialMaps; i++)
            {
                AddMap();
            }
        }

        /// <summary>
        /// キルゾーンを作る
        /// </summary>
        public void CreateKillZone()
        {
            PhotonNetwork.Instantiate(_killZonePrefabName, _killZoneInitialPosition, Quaternion.identity);
        }

        /// <summary>
        /// マップを更新する
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="map">プレイヤーが触れたマップ</param>
        public void UpdateMaps(Player player, Map map)
        {
            // マップが増やせる場合
            if (map.CanAddMap)
            {
                // マップを増やす
                AddMap();

                // マップに再び触れても増やせないようにする
                map.CanAddMap = false;
            }

            // 最下位のプレイヤーが触れた場合
            if (player.IsInTheLowestPosition)
            {
                // マップを削除する
                RemoveMap(map.Id - _idOffsetOfmapToRemove);
            }

        }

        /// <summary>
        /// マップを増やす
        /// </summary>
        public void AddMap()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            // 位置
            Vector3 position = new(0.0f, MapCreationCount * _offsetY, 0.0f);

            // インスタンス
            GameObject instance = PhotonNetwork.Instantiate(_mapPrefabName, position, Quaternion.identity);

            // マップのIDを設定する
            Map map = instance.GetComponent<Map>();
            map.SetAndSynchronizeId(MapCreationCount);

            // マップ作成数を増やす
            MapCreationCount++;
        }

        /// <summary>
        /// マップを削除する
        /// </summary>
        /// <param name="id">削除対象のID</param>
        public void RemoveMap(int id)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            Map map = GameObject.FindGameObjectsWithTag("Map")
                .Select(x => x.GetComponent<Map>())
                .FirstOrDefault(x => x.Id == id);

            if (map)
            {
                PhotonNetwork.Destroy(map.gameObject);
            }
        }
    }
}
