namespace SharedKernel.Result
{
    public class Result<TValue> : IResult
    {
        private Result(ResultStatus status)
        {
            Status = status;
            ValidationErrors = new List<ValidationError>();
            SuccessMessage = "";
        }

        public Result(TValue value)
            : this(ResultStatus.Ok)
        {
            Value = value;
            ValueType = value?.GetType();
        }

        #region IResult

        public ResultStatus Status { get; private set; }

        public List<ValidationError> ValidationErrors { get; private set; }

        public Type ValueType { get; private set; }

        public object GetValue()
        {
            return Value;
        }

        #endregion

        public void ClearValueType()
        {
            ValueType = null;
        }

        public TValue Value { get; }

        public bool IsSuccess
        {
            get { return Status == ResultStatus.Ok; }
        }

        public string SuccessMessage { get; private set; }

        /// <summary>
        /// Converts this Result into a PagedResult<typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="pagedInfo">Paged info for the new result.</param>
        /// <returns></returns>
        public PagedResult<TValue> ToPagedResult(PagedInfo pagedInfo)
        {
            return new PagedResult<TValue>(pagedInfo, Value)
            {
                Status = Status,
                SuccessMessage = SuccessMessage,
                ValidationErrors = ValidationErrors
            };
        }

        #region Statics Methods

        /// <summary>
        /// Represents a successful operation with a value as the result of the operation.
        /// </summary>
        /// <param name="value">Value as result of the operation.</param>
        /// <returns>A Result<typeparamref name="TValue"/>.</returns>
        public static Result<TValue> Success(TValue value)
        {
            return new Result<TValue>(value);
        }

        /// <summary>
        /// Represents a successful operation with a value as the result of the operation 
        /// and a specific success message.
        /// </summary>
        /// <param name="value">Value as result of the operation.</param>
        /// <param name="successMessage">Defined success message.</param>
        /// <returns>A Result<typeparamref name="TValue"/>.</returns>
        public static Result<TValue> Success(TValue value, string successMessage)
        {
            return new Result<TValue>(value) { SuccessMessage = successMessage };
        }

        /// <summary>
        /// Represents validation errors that prevented the underlying operation from completing.
        /// </summary>
        /// <param name="validationErrors">A list of validation errors encountered.</param>
        /// <returns>A Result<typeparamref name="TValue"/>.</returns>
        public static Result<TValue> Invalid(List<ValidationError> validationErrors)
        {
            return new Result<TValue>(ResultStatus.Invalid) { ValidationErrors = validationErrors };
        }

        /// <summary>
        /// This is similar to Forbidden, but should be used when the user has not authenticated or has attempted to authenticate but failed.
        /// See also HTTP 401 Unauthorized: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
        /// </summary>
        /// <returns>A Result<typeparamref name="TValue"/>.</returns>
        public static Result<TValue> Unauthorized()
        {
            return new Result<TValue>(ResultStatus.Unauthorized);
        }

        /// <summary>
        /// The parameters to the call were correct, but the user does not have permission to perform some action.
        /// See also HTTP 403 Forbidden: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
        /// </summary>
        /// <returns>A Result<typeparamref name="TValue"/>.</returns>
        public static Result<TValue> Forbidden()
        {
            return new Result<TValue>(ResultStatus.Forbidden);
        }

        /// <summary>
        /// Represents the situation where an operation was unable to find a requested resource.
        /// </summary>
        /// <returns>A Result<typeparamref name="TValue"/>.</returns>
        public static Result<TValue> NotFound()
        {
            return new Result<TValue>(ResultStatus.NotFound);
        }

        #endregion

        #region Operators

        public static implicit operator TValue(Result<TValue> result)
        {
            return result.Value;
        }

        public static implicit operator Result<TValue>(TValue value)
        {
            return Success(value);
        }

        #endregion
    }
}