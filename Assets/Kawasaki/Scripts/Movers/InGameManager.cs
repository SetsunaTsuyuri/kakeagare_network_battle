using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using SetsunaTsuyuri;

namespace Kawasaki
{
    /// <summary>
    /// インゲームの管理者
    /// </summary>
    public class InGameManager : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        private void Start()
        {
            // BGMを再生する
            AudioManager.PlayBGM("InGame");

        }

        void IOnEventCallback.OnEvent(EventData photonEvent)
        {
            // やられたイベントは 2 とする
            if (photonEvent.Code == 2)
            {
                int killedPlayerActorNumber = (int)photonEvent.CustomData;
                print($"Player {photonEvent.Sender} retired.");

                // やられたのが自分だったら自分を消す
                if (killedPlayerActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    GameObject me = players.Where(x => x.GetPhotonView().IsMine).FirstOrDefault();
                    PhotonView view = me.GetPhotonView();
                    PhotonNetwork.Destroy(view);
                }
            }
        }
    }
}
