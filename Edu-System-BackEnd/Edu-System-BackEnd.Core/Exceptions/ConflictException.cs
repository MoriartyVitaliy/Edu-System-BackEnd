using System.Net;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions
{
    public class ConflictException : BaseException
    {
        public ConflictException(string message) : base(message, (int)HttpStatusCode.Conflict)
        {
        }
    }
}
