using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBlock : MonoBehaviour
{
    [SerializeField, Tooltip("ゲームが開始するまで特定範囲から出られなくするブロックのプレハブ")]
    GameObject _blockPref = null;

    [SerializeField, Tooltip("スポーン地点の位置")]
    Transform _SpawnPoint = null;

    [SerializeField, Tooltip("スポーン地点からどのくらい上に配置するかを指定")]
    float _offsetFromSpawnPosition = 4f;

    /// <summary>生成したブロック実体</summary>
    GameObject _block = null;

    /// <summary>スタートブロックを置く</summary>
    public void CreateBlock()
    {
        _block = Instantiate(_blockPref, _SpawnPoint.position + Vector3.up * _offsetFromSpawnPosition, _SpawnPoint.rotation);
    }

    /// <summary>スタートブロックを消す</summary>
    public void DestroyBlock()
    {
        Destroy(_block);
    }
}
