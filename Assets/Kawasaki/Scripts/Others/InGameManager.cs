using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
        /// <summary>
        /// 試合結果の情報
        /// </summary>
        GameResult _gameResult = new();

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
                OnGameEnd(photonEvent);
            }
        }

        /// <summary>
        /// 試合終了時の処理
        /// </summary>
        /// <param name="data"></param>
        private void OnGameEnd(EventData data)
        {
            // 試合結果
            _gameResult = new GameResult();

            int killedPlayerActorNumber = (int)data.CustomData;
            print($"Player {data.Sender} retired.");

            // やられたのが自分だったら自分を消す
            if (killedPlayerActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                GameObject me = players.Where(x => x.GetPhotonView().IsMine).FirstOrDefault();
                PhotonView view = me.GetPhotonView();
                PhotonNetwork.Destroy(view);

                // 敗北
                _gameResult.Win = false;

                Debug.Log("敗北");
            }
            else
            {
                // 勝利
                _gameResult.Win = true;

                Debug.Log("勝利");
            }

            // リザルトシーンの処理
            // リザルトシーンの名前と遷移先で実行したい関数を渡す
            SceneChangeManager.StartSceneChange("Result", OnResultSceneLoaded);
        }

        private void OnResultSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // リザルト表示クラスに試合結果を渡す
            Tsuguhiko.ResultDisplay resultDisplay = FindObjectOfType<Tsuguhiko.ResultDisplay>();
            if (resultDisplay)
            {
                resultDisplay.ResultJudge = _gameResult;
            }
        }
    }
}
