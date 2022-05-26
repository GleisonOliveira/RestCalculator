using RestCalculator.Resources.Shared;
using System.Resources;

namespace RestCalculator.Exceptions
{
    public abstract class BaseException : ApplicationException
    {
        protected readonly object? _controller;

        protected readonly object? _action;

        public int Status { get; protected set; }

        public string? Detail { get; protected set; }

        public string? Title { get; protected set; }

        public string? Instance { get; protected set; }

        public string? Controller { get; protected set; }

        public string? Action { get; protected set; }

        public Dictionary<string, string[]>? Errors { get; protected set; }


        public BaseException(
            string detail,
            int statusCode = 400
        ) : base(detail)
        {
            Status = statusCode;
            Detail = detail;

            var erros = new[] { "teste", "teste2" };
            Errors = new Dictionary<string, string[]>();
            Errors.Add("primeiro", erros);
        }

        public BaseException(
            string detail,
            string? title,
            int statusCode = 400
        ) : this(detail, statusCode)
        {
            Title = title;
        }

        public BaseException(
            string detail,
            string? title,
            string? controller,
            int statusCode = 400
        ) : this(detail, title, statusCode)
        {
            Controller = controller;
        }

        public BaseException(
            string detail,
            string? title,
            string? controller,
            string? action,
            int statusCode = 400
        ) : this(detail, title, controller, statusCode)
        {
            Action = action;
        }

        public BaseException(
             string detail,
             string? title,
             Dictionary<string, string[]>? errors,
             int statusCode = 400
         ) : this(detail, title, statusCode)
        {
            Errors = errors;
        }
    }
}
