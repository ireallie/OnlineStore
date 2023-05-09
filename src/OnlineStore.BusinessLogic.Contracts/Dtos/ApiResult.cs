namespace OnlineStore.BusinessLogic.Contracts.Dtos
{
    public class ApiResult<T>
    {
        private ApiResult() { }

        private ApiResult(bool isSucceeded, T data, IEnumerable<string> errors)
        {
            IsSucceeded = isSucceeded;
            Data = data;
            Errors = errors;
        }

        public bool IsSucceeded { get; set; }

        public T Data { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public static ApiResult<T> Success(T data)
        {
            return new ApiResult<T>(true, data, new List<string>());
        }

        public static ApiResult<T> Failure(IEnumerable<string> errors)
        {
            return new ApiResult<T>(false, default, errors);
        }
    }
}
