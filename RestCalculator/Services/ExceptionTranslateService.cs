using Microsoft.AspNetCore.Mvc;
using RestCalculator.Exceptions;
using RestCalculator.Resources.Shared;
using System.Resources;

namespace RestCalculator.Services
{
    public class ExceptionTranslateService
    {
        private readonly ResourceManager _resourceManager;

        private readonly BaseException _exception;
        public ExceptionTranslateService(BaseException exception)
        {
            _exception = exception;
            _resourceManager = new ResourceManager(typeof(SharedResources));
        }

        /// <summary>
        /// Convert an exception in a problem details
        /// </summary>
        /// <returns></returns>
        public ProblemDetails convertExceptionToProblemDetails()
        {
            var problemDetails = new ValidationProblemDetails()
            {
                Detail = translateProperty(_exception.Detail),
                Instance = _exception.Controller,
                Status = _exception.Status,
                Title = translateProperty(_exception.Title),
                Type = _exception.Action,
            };

            foreach(var error in _exception.Errors)
            {
                problemDetails.Errors.Add(error.Key, translateErrors(error.Value));
            }

            return problemDetails;
        }

        /// <summary>
        /// Translate a property if is in resource
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private string? translateProperty(string? property)
        {
            string? result = null;

            if (_exception.Title != null)
            {
                string controller = _exception.Controller ?? "";
                string action = _exception.Action ?? "";

                try
                {
                    string? format = string.Format("{0}.{1}.{2}", controller, action, property);

                    return _resourceManager.GetString(format);
                }
                catch
                {
                    result = property;
                }

                try
                {
                    string? format = string.Format("{0}", property);

                    return _resourceManager.GetString(format);
                }
                catch
                {
                    result = property;
                }
            }

            return result;
        }

        private string[] translateErrors(string[] items)
        {
            var result = items;

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = translateProperty(result[i]) ?? "";
            }

            return result;
        }
    }
}
