namespace Kawasaki
{
    /// <summary>
    /// 気絶する
    /// </summary>
    public interface IStunned
    {
        /// <summary>
        /// 気絶する
        /// </summary>
        /// <param name="duration">持続時間</param>
        void BeStunned(float duration);
    }
}
