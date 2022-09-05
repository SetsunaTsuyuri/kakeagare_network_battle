using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace SetsunaTsuyuri
{
    /// <summary>
    /// プレハブローダー
    /// </summary>
    public class FadeManagerLoader : SingletonPrefabLoader<FadeManager>
    {
        public override FadeManager LoadPrefab()
        {
            return Resources.Load<FadeManager>("SetsunaTsuyuri/Common/Fade");
        }
    }

    /// <summary>
    /// フェード演出の管理者
    /// </summary>
    public class FadeManager : SingletonMonoBehaviour<FadeManager, FadeManagerLoader>, IInitializable
    {
        /// <summary>
        /// フェードの状態
        /// </summary>
        private enum FadeState
        {
            /// <summary>
            /// なし
            /// </summary>
            None = 0,

            /// <summary>
            /// フェードイン
            /// </summary>
            FadeIn = 1,

            /// <summary>
            /// フェードアウト
            /// </summary>
            FadeOut = 2
        }

        /// <summary>
        /// 現在のフェード状態
        /// </summary>
        FadeState state = FadeState.None;

        /// <summary>
        /// フェード時間
        /// </summary>
        [SerializeField]
        float fadeDuration = 0.2f;

        /// <summary>
        /// フェードアウトのマテリアル
        /// </summary>
        [SerializeField]
        Material fadeOut = null;

        /// <summary>
        /// フェードインのマテリアル
        /// </summary>
        [SerializeField]
        Material fadeIn = null;

        /// <summary>
        /// フェードするスプライト
        /// </summary>
        [SerializeField]
        Sprite sprite = null;

        /// <summary>
        /// フェードするイメージ
        /// </summary>
        Image image = null;

        public override void Initialize()
        {
            image = GetComponentInChildren<Image>();
            image.sprite = sprite;
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async UniTask FadeOut(CancellationToken token)
        {
            await Instance.FadeOutInner(token);
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async UniTask FadeOutInner(CancellationToken token)
        {
            if (state == FadeState.FadeOut)
            {
                return;
            }

            await Fade(fadeOut, fadeDuration, token);

            state = FadeState.FadeOut;
        }

        /// <summary>
        /// フェードイン
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async UniTask FadeIn(CancellationToken token)
        {
            await Instance.FadeInInner(token);
        }

        /// <summary>
        /// フェードイン
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async UniTask FadeInInner(CancellationToken token)
        {
            if (state == FadeState.FadeIn)
            {
                return;
            }

            await Fade(fadeIn, fadeDuration, token);

            state = FadeState.FadeIn;
        }

        /// <summary>
        /// フェード処理を行う
        /// </summary>
        /// <param name="material">マテリアル</param>
        /// <param name="duration">フェード時間</param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async UniTask Fade(Material material, float duration, CancellationToken token)
        {
            image.material = material;

            float time = 0.0f;
            while (time < duration)
            {
                material.SetFloat("_Alpha", time / duration);
                await UniTask.Yield(token);
                time += Time.deltaTime;
            }
            material.SetFloat("_Alpha", 1);
        }
    }
}
