using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// 当たったら死ぬオブジェクト
/// </summary>
[RequireComponent(typeof(PhotonView), typeof(Collider2D))]
public class KillZone : MonoBehaviour
{
    /// <summary>初期スピード</summary>
    [SerializeField] float _initialSpeed = 0f;
    /// <summary>加速度</summary>
    [SerializeField] float _acceleration = 0.0001f;
    bool _isGameOver = false;
    PhotonView _view;

    private void Start()
    {
        _view = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        // ゲームオーバーになったら動かさない
        if (!_isGameOver && _view.IsMine)
        {
            //transform.Translate(0, _initialSpeed, 0);
            //_initialSpeed += _acceleration;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // オーナー側で判定する
        if (collision.tag == "Player" && !_isGameOver && _view.IsMine)
        {
            _isGameOver = true;
            // 誰が接触したかを判定し、イベント通知する
            int actorNum = collision.GetComponent<PhotonView>().OwnerActorNr;
            Debug.Log($"KillZone caught player {actorNum}");
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
            raiseEventOptions.Receivers = ReceiverGroup.All;
            SendOptions sendOptions = new SendOptions();
            PhotonNetwork.RaiseEvent(2, actorNum, raiseEventOptions, sendOptions);
        }
    }
}
