using Common.Enums;

namespace Common.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public override int HttpStatusCode => 401;
        public override ErrorCodes? ErrorCode { get; } = ErrorCodes.Unauthorized;

        public UnauthorizedException() { }

        public UnauthorizedException(string message)
            : base(message)
        { }

        public UnauthorizedException(ErrorCodes errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
