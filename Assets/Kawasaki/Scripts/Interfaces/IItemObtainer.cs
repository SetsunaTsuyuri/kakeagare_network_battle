namespace Kawasaki
{
    public interface IItemObtainer
    {
        /// <summary>
        /// アイテムを入手する
        /// </summary>
        /// <param name="item">アイテム</param>
        void Obtain(Item item);
    }
}
