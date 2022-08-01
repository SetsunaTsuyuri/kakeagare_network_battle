using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SetsunaTsuyuri
{
    /// <summary>
    /// シングルトンのプレハブをロードする者
    /// </summary>
    /// <typeparam name="TSingleton">シングルトンの型</typeparam>
    public abstract class SingletonPrefabLoader<TSingleton>
        where TSingleton : MonoBehaviour, IInitializable
    {
        /// <summary>
        /// プレハブをロードする
        /// </summary>
        /// <returns></returns>
        public abstract TSingleton LoadPrefab();
    }

    /// <summary>
    /// シングルトンなMonoBehaviour
    /// </summary>
    /// <typeparam name="TSingleton">シングルトンの型</typeparam>
    /// <typeparam name="TLoader">プレハブローダーの型</typeparam>
    public abstract class SingletonMonoBehaviour<TSingleton, TLoader> : MonoBehaviour
        where TSingleton : MonoBehaviour, IInitializable
        where TLoader : SingletonPrefabLoader<TSingleton>, new()
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        static TSingleton instance = null;

        /// <summary>
        /// インスタンス
        /// </summary>
        public static TSingleton Instance
        {
            get
            {
                // nullの場合
                if (instance is null)
                {
                    // プレハブをロードする
                    TLoader loader = new TLoader();
                    TSingleton prefab = loader.LoadPrefab();

                    // ロードしたプレハブをインスタンス化し、クラス変数に代入する
                    instance = Instantiate(prefab);

                    // シーン遷移の際、インスタンスを破壊されないようにする
                    DontDestroyOnLoad(instance);

                    // インスタンスを初期化する
                    instance.Initialize();
                }

                return instance;
            }
        }

        public virtual void Initialize() { }
    }
}
