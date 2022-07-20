using Common.Enums;

namespace Common.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public override int HttpStatusCode => 403;
        public override ErrorCodes? ErrorCode { get; } = ErrorCodes.Forbidden;

        public ForbiddenException() { }

        public ForbiddenException(string message)
            : base(message)
        { }

        public ForbiddenException(ErrorCodes errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
