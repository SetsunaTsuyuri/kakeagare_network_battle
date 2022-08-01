using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SetsunaTsuyuri
{
    /// <summary>
    /// IEnumerableの拡張メソッド
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 要素をランダムに並び変える
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(i => Guid.NewGuid());
        }

        /// <summary>
        /// 指定した型と同じ要素を取得する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="type">型</param>
        /// <returns></returns>
        public static T GetSameType<T>(this IEnumerable<T> collection, Type type)
        {
            return collection.FirstOrDefault(v => v.GetType() == type);
        }

        /// <summary>
        /// 指定した型と同じ要素が存在する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="type">型</param>
        /// <returns></returns>
        public static bool ExistsSameType<T>(this IEnumerable<T> collection, Type type)
        {
            return collection.Any(s => s.GetType() == type);
        }

        /// <summary>
        /// 範囲外を指定している
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public static bool OutOfRange<T>(this IEnumerable<T> collection, int index)
        {
            return index < 0 || index >= collection.Count();
        }

        /// <summary>
        /// 指定した要素または初期値を取得する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="collecion">コレクション</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(this IEnumerable<T> collecion, int index)
        {
            T result = default;
            if (!collecion.OutOfRange(index))
            {
                result = collecion.ElementAt(index);
            }

            return result;
        }

        /// <summary>
        /// 指定した要素を取得する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="collection">コレクション</param>
        /// <param name="index">索引</param>
        /// <param name="value">値</param>
        /// <returns></returns>
        public static bool TryGetValue<T>(this IEnumerable<T> collection, int index, out T value)
        {
            value = default;
            bool result = false;

            if (!collection.OutOfRange(index))
            {
                value = collection.ElementAt(index);
                result = true;
            }

            return result;
        }
    }
}
