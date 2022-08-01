using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

namespace Tsuguhiko
{
    public class RankDisplay : MonoBehaviourPunCallbacks
    {
        [SerializeField, Header("ランクのテキスト")] TextMeshProUGUI _rankText;

        [SerializeField,Header("ランク")] Rank _playerRank;

        PhotonView _viewRank;

        void Start()
        {
            _viewRank = GetComponent<PhotonView>();
        }

        
        void Update()
        {
            
            if (photonView.IsMine)
            {
                if (_playerRank == Rank.First)
                {
                    _rankText.text = "1st";

                    _rankText.color = Color.blue;
                }
                else if (_playerRank == Rank.Second)
                {
                    _rankText.text = "2nd";

                    _rankText.color = Color.red;
                }
            }
        }

        //void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        //{
        //    if (stream.IsWriting)
        //    {
        //        stream.SendNext(_rankText);
        //    }
        //    else
        //    {
        //        _rankText = (TextMeshProUGUI)stream.ReceiveNext();
        //    }
        //}
    }
}