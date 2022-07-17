using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// PlayerMovementの拡張関数集
    /// </summary>
    public static class PlayerMovementExtensions
    {
        /// <summary>
        /// 回転(Y軸)を設定する
        /// </summary>
        /// <param name="playerMovement"></param>
        /// <param name="defaultRotationY">初期のY軸角度</param>
        /// <param name="axisHorizontal">仮想軸(水平)</param>
        public static void SetRotationY(this Karaki.PlayerMovement playerMovement, float defaultRotationY, float axisHorizontal)
        {
            // 気絶時間が残っている場合は終了する
            if (playerMovement.StunTimeCount > 0.0f)
            {
                return;
            }

            // 仮想軸(水平)の値に応じてY軸の角度を設定する
            if (axisHorizontal != 0.0f)
            {
                Quaternion newRotation = playerMovement.transform.rotation;
                if (axisHorizontal > 0.0f)
                {
                    newRotation.y = defaultRotationY;
                }
                else if (axisHorizontal < 0.0f)
                {
                    newRotation.y = defaultRotationY + 180.0f;
                }
                playerMovement.transform.rotation = newRotation;
            }
        }
    }
}
