using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public abstract class BaseException : Exception
    {
        public abstract ErrorCodes? ErrorCode { get; }
        public abstract int HttpStatusCode { get; }

        public BaseException() { }
        public BaseException(string message) : base(message) { }
    }
}
