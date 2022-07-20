using Common.Enums;
using Models.Responses.Interfaces;

namespace Models.Responses
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        #region Constructor

        public ApiResponse()
        {
            HttpCode = 200;
        }

        #endregion

        #region Public Methods

        public bool IsSuccessful => (this.HttpCode == 200);
        public int HttpCode { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public string Exception { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }

        #endregion
    }

    public class ApiResult<T>
    {
        public T Value { get; set; }
    }

    public class ApiListResult<T>
    {
        public T Items { get; set; }
    }
}
