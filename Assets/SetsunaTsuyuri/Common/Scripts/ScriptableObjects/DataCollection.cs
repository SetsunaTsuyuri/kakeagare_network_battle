using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SetsunaTsuyuri
{
    /// <summary>
    /// データ集
    /// </summary>
    /// <typeparam name="T">データの型</typeparam>
    public abstract class DataCollection<T> : ScriptableObject
    {
        /// <summary>
        /// データ
        /// </summary>
        [field: SerializeField]
        public T[] Data { get; private set; }

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetValue(int index)
        {
            return Data[index];
        }

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>見つからなければ初期値を返す</returns>
        public T GetValueOrDefault(int index)
        {
            return Data.GetValueOrDefault(index);
        }

        /// <summary>
        /// データの取得を試みる
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="value">データ</param>
        /// <returns></returns>
        public bool TryGetValue(int index, out T value)
        {
            return Data.TryGetValue(index, out value);
        }
    }
}
