using Common.Enums;

namespace Common.Exceptions
{
    public class BadRequestException : BaseException
    {
        public override int HttpStatusCode => 400;
        public override ErrorCodes? ErrorCode { get; } = ErrorCodes.BadRequest;

        public BadRequestException() { }

        public BadRequestException(string message)
            : base(message)
        { }

        public BadRequestException(ErrorCodes errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
