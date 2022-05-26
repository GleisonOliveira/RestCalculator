using Microsoft.AspNetCore.Mvc;
using RestCalculator.Resources.Shared;
using System.Resources;

namespace RestCalculator.Errors
{
    public class BadRequestError : ValidationProblemDetails
    {
        private readonly ResourceManager _resourceManager;

        private readonly object? _controller;

        private readonly object? _action;

        public BadRequestError(ActionContext context) : base(context.ModelState)
        {
            _resourceManager = new ResourceManager(typeof(SharedResources));

            _controller = context.RouteData.Values["controller"];
            _action = context.RouteData.Values["action"];

            Title = getTitle();
            Detail = getDetail();
            Status = 400;
            Instance = context.HttpContext.TraceIdentifier;
            Detail = _action != null && _controller != null ? string.Format("{0}.{1}", _controller, _action) : null;
            this.Type = _action != null ? _action.ToString() : null;

            translateErrors(context);
        }

        /// <summary>
        /// Get the details
        /// </summary>
        /// <returns></returns>
        private string? getDetail()
        {
            if (Detail != null)
            {
                try
                {
                    return _resourceManager.GetString(string.Format("{0}.{1}.{2}", _controller, _action, Detail));
                }
                catch
                {
                    Detail = Detail;
                }

                try
                {
                    return _resourceManager.GetString(string.Format("{0}", Detail));
                }
                catch
                {
                    Detail = Detail;
                }
            }

            return Detail;
        }

        /// <summary>
        /// Get the title
        /// </summary>
        /// <returns></returns>
        private string? getTitle()
        {
            if (Title != null)
            {
                try
                {
                    return _resourceManager.GetString(string.Format("{0}.{1}.{2}", _controller, _action, Title));
                }
                catch
                {
                    Title = Title;
                }

                try
                {
                    return _resourceManager.GetString(string.Format("{0}", Title));
                }
                catch
                {
                    Title = Title;
                }
            }

            return Title;
        }

        /// <summary>
        /// Translate all errors
        /// </summary>
        /// <param name="context"></param>
        private void translateErrors(ActionContext context)
        {
            var keys = context.ModelState.Keys;


            foreach (var key in keys)
            {
                if (_controller != null && _action != null)
                {
                    if (Errors.ContainsKey(key))
                    {
                        for (int i = 0; i < Errors[key].Length; i++)
                        {
                            string translation = _resourceManager.GetString(string.Format("{0}.{1}.{2}", _controller.ToString(), _action.ToString(), key.ToString())) ?? "";

                            if (!string.IsNullOrWhiteSpace(translation))
                            {
                                Errors[key][i] = translation;
                            }
                        }
                    }
                }
            }
        }
    }
}
