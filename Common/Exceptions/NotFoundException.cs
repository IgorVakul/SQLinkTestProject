using Common.Enums;

namespace Common.Exceptions
{
    public class NotFoundException : BaseException
    {
        public override int HttpStatusCode => 404;
        public override ErrorCodes? ErrorCode { get; } = ErrorCodes.NotFound;

        public NotFoundException() { }

        public NotFoundException(ErrorCodes errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
