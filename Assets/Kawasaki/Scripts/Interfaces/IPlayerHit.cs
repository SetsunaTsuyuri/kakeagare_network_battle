
namespace Kawasaki
{
    /// <summary>
    /// プレイヤーに触れた時に何かが起きる
    /// </summary>
    public interface IPlayerHit
    {
        /// <summary>
        /// プレイヤーに触れた時の処理
        /// </summary>
        /// <param name="player">プレイヤー</param>
        void OnHit(Player player);
    }
}
