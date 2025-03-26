using System.Net;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message = "resource not found") 
            : base(message, (int)HttpStatusCode.NotFound, "Not Found") { }
    }
}
