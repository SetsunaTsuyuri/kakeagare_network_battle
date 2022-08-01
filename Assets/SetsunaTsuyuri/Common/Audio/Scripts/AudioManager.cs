using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace SetsunaTsuyuri
{
    public class AudioManagerLoader : SingletonPrefabLoader<AudioManager>
    {
        public override AudioManager LoadPrefab()
        {
            return Resources.Load<AudioManager>("SetsunaTsuyuri/Common/Audio");
        }
    }

    /// <summary>
    /// 音声の管理者
    /// </summary>
    public class AudioManager : SingletonMonoBehaviour<AudioManager, AudioManagerLoader>, IInitializable
    {
        /// <summary>
        //// BGMの制御者
        //// </summary>
        [SerializeField]
        AudioSourceController bgmSource = null;

        /// <summary>
        /// SEの制御者
        /// </summary>
        [SerializeField]
        AudioSourceController seSource = null;

        /// <summary>
        /// BGMデータ集
        /// </summary>
        [SerializeField]
        AudioDataCollection bgmClips = null;

        /// <summary>
        /// SEデータ集
        /// </summary>
        [SerializeField]
        AudioDataCollection seClips = null;

        /// <summary>
        /// BGMのキャンセレーショントークンソース
        /// </summary>
        CancellationTokenSource bgmCancellationTokenSource = null;

        private void OnDestroy()
        {
            bgmCancellationTokenSource?.Cancel();
        }

        /// <summary>
        /// BGMを再生する
        /// </summary>
        /// <param name="name"></param>
        public static void PlayBGM(string name, float duration = 0.0f)
        {
            Instance.PlayBGMInner(name, duration);
        }

        /// <summary>
        /// BGMを再生する
        /// </summary>
        /// <param name="name">BGMの名前</param>
        /// <param name="duration">フェード時間</param>
        private void PlayBGMInner(string name, float duration)
        {
            AudioData data = bgmClips.GetDataOrDefault(name);
            if (data is null)
            {
                Debug.LogError($"{name} is not found");
                return;
            }

            bgmSource.Data = data;

            bgmCancellationTokenSource?.Cancel();
            bgmCancellationTokenSource = new CancellationTokenSource();

            bgmSource.PlayWithFadeIn(duration, bgmCancellationTokenSource.Token).Forget();

            // ------------MEMO--------------------------------------------
            // 演奏中のBGMがある場合
            // Play →即時停止
            // Cross(未実装) →fadeInDurationと同じDurationでfadeout
            // Stop 演奏中のBGMがあればフェードアウト なければ何もしない

            // private protectedな関数
            // 複数のインターフェースを実装し、関数名が被ったら
            // BGMデータ配列からオーディオデータを取得
            // フェードor即時 クロスフェードの場合 再生中でないオーディオソースコントローラを取得 フェードイン再生
            // 再生中のオーディオソースコントローラーをフェードアウト
            //-------------------------------------------------------------
        }

        /// <summary>
        /// BGMを止める
        /// </summary>
        /// <param name="duration">フェード時間</param>
        public static void StopBGM(float duration = 0.5f)
        {
            Instance.StopBGMInner(duration);
        }

        /// <summary>
        /// BGMを止める
        /// </summary>
        /// <param name="duration">フェード時間</param>
        private void StopBGMInner(float duration)
        {
            bgmCancellationTokenSource?.Cancel();
            bgmCancellationTokenSource = new CancellationTokenSource();

            bgmSource.StopWithFadeOut(duration, bgmCancellationTokenSource.Token).Forget();
        }

        /// <summary>
        /// 効果音(SE)を再生する
        /// </summary>
        /// <param name="name">SEの名前</param>
        public static void PlaySE(string name)
        {
            Instance.PlaySEInner(name);
        }

        /// <summary>
        /// 効果音(SE)を再生する
        /// </summary>
        /// <param name="name">SEの名前</param>
        private void PlaySEInner(string name)
        {
            AudioData data = seClips.GetDataOrDefault(name);
            if (data is null)
            {
                Debug.LogError($"{name} is not found");
                return;
            }

            seSource.Data = data;
            seSource.PlayOneShot();
        }
    }
}
