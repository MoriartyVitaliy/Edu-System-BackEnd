namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Exceptions
{
    public abstract class BaseException : Exception
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string ExceptionDetail { get; set; }
        protected BaseException(string message, int statusCode, string title = "Error") : base(message)
        {
            StatusCode = statusCode;
            Title = title;
            ExceptionDetail = message;
        }
    }
}
