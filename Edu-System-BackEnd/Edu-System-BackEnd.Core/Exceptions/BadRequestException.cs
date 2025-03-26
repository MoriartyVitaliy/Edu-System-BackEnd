using System.Net;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message = "Validation error") 
            : base(message, (int)HttpStatusCode.BadRequest, "Bad request situation")
        {
        }
    }
}
