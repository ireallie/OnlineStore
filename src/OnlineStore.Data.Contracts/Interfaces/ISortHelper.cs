namespace OnlineStore.Data.Contracts.Interfaces
{
    public interface ISortHelper<T> where T : class
    {
        /// <summary>
        /// Should apply sorting if needed.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ordering"></param>
        /// <returns></returns>
        IQueryable<T> ApplySorting(IQueryable<T> query, string ordering);
    }
}
