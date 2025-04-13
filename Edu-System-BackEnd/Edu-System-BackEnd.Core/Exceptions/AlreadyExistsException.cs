using System.Net;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions
{
    public class AlreadyExistsException : BaseException
    {
        public AlreadyExistsException(string message = "Entity already exist") 
            : base(message, (int)HttpStatusCode.Conflict, "Conflict situation via database")
        {
        }
    }
}