using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

namespace SetsunaTsuyuri
{
    /// <summary>
    /// シーン遷移の管理者
    /// </summary>
    public class SceneChangeManager : Singleton<SceneChangeManager>, IInitializable
    {
        /// <summary>
        /// シーン切り替えの最中である
        /// </summary>
        bool _isChangingScene = false;

        /// <summary>
        /// シーンを変更する
        /// </summary>
        /// <param name="name">シーンの名前</param>
        /// <param name="callback">シーン変更後に呼び出す関数</param>
        public static void ChangeScene(string name, UnityAction<Scene, LoadSceneMode> callback = null)
        {
            // シーン変更中なら中止する
            if (Instance._isChangingScene)
            {
                return;
            }

            CancellationTokenSource source = new();
            Instance.ChangeSceneAsync(name, callback, source.Token).Forget();
        }

        /// <summary>
        /// シーンを変更する(非同期)
        /// </summary>
        /// <param name="name">シーンの名前</param>
        /// <param name="callback">シーン変更後に呼び出す関数</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async UniTask ChangeSceneAsync(string name, UnityAction<Scene, LoadSceneMode> callback, CancellationToken token)
        {
            // フラグON
            _isChangingScene = true;

            // フェードアウト
            await FadeManager.FadeOut(token);

            // 関数登録
            if (callback is not null)
            {
                SceneManager.sceneLoaded += callback;
            }

            // シーンロード
            SceneManager.LoadScene(name);

            // フェードイン
            await FadeManager.FadeIn(token);

            // フラグOFF
            _isChangingScene = false;
        }
    }
}
