namespace SharedKernel.Result
{
    public class PagedResult<TValue> : Result<TValue>
    {
        public PagedResult(PagedInfo pagedInfo, TValue value) 
            : base(value)
        {
            PagedInfo = pagedInfo;
        }

        public PagedInfo PagedInfo { get; }
    }
}
