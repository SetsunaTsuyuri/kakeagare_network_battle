using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class RankDisplay : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField, Header("ランクのテキスト")] TextMeshProUGUI _rankText;

    Rank _playerRank;



    void Start()
    {

    }
    void Update()
    {
        if (photonView.IsMine)
        {
            if (_playerRank == Rank.First)
            {
                _rankText.text = "1st";
            }
            else if (_playerRank == Rank.Second)
            {
                _rankText.text = "2nd";
            }
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_rankText);
        }
        else
        {
            _rankText = (TextMeshProUGUI)stream.ReceiveNext();
        }
    }
}
