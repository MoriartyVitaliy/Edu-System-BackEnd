using System.Net;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message = "Access denied") 
            : base(message, (int)HttpStatusCode.Unauthorized, "Unauthorized"){  }
    }
}
