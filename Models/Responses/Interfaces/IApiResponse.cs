using Common.Enums;

namespace Models.Responses.Interfaces
{
    public interface IApiResponse<T>
    {
        public bool IsSuccessful { get; }

        public int HttpCode { get; set; }

        public ErrorCodes ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string Exception { get; set; }

        public T Data { get; set; }
    }
}
