using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

/// <summary>
/// マップを生成する
/// とりあえずプレイヤーにアタッチしておく
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class MapGenerator : MonoBehaviour
{
    /// <summary>マップを構成するプレハブの名前</summary>
	[SerializeField] string _mapPrefabName;
    /// <summary>マップのプレハブをいくらおきに設置するか</summary>
    [SerializeField] float _offsetY = 10f;
    /// <summary>KillZone のプレハブ名</summary>
    [SerializeField] string _killZonePrefabName;
    /// <summary>KillZone の初期位置</summary>
    [SerializeField] Vector3 _killZoneInitialPosition;
    PhotonView _view;

    private void Start()
    {
        _view = GetComponent<PhotonView>();

        // マップと KillZone はマスタークライアント側で生成する
        if (PhotonNetwork.IsMasterClient)
        {
            // 自分がログインした時はマップを生成する
            if (_view.IsMine)
            {
                BuildMap();
            }
            else  // 二人目が入ってきた時に KillZone を生成する
            {
                GenerateKillZone();
            }
        }
    }

    /// <summary>
    /// マップを生成する
    /// </summary>
    void BuildMap()
    {
        for (int i = 0; i < 10; i++)
        {
            PhotonNetwork.Instantiate(_mapPrefabName, new Vector3(0, i * _offsetY, 0), Quaternion.identity);
        }
    }

    /// <summary>
    /// KillZone を設置する
    /// </summary>
    void GenerateKillZone()
    {
        PhotonNetwork.Instantiate(_killZonePrefabName, _killZoneInitialPosition, Quaternion.identity);
    }
}
