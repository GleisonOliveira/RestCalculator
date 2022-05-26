using System.Resources;

namespace RestCalculator.Exceptions
{
    public class InvalidRequestException : BaseException
    {
        public InvalidRequestException(string detail, int statusCode = 400) : base(detail, statusCode)
        {
        }

        public InvalidRequestException(string detail, string? title, int statusCode = 400) : base(detail, title, statusCode)
        {
        }

        public InvalidRequestException(string detail, string? title, string? controller, int statusCode = 400) : base(detail, title, controller, statusCode)
        {
        }

        public InvalidRequestException(string detail, string? title, Dictionary<string, string[]>? errors, int statusCode = 400) : base(detail, title, errors, statusCode)
        {
        }

        public InvalidRequestException(string detail, string? title, string? controller, string? action, int statusCode = 400) : base(detail, title, controller, action, statusCode)
        {
        }
    }
}
