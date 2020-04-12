using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SQLServer.Exceptions;
using System;
using System.Collections.Generic;

namespace MusicMatch_Server.FIlters
{
    public class ValidationFilter : IActionFilter, IOrderedFilter
    {

        public int Order => 0;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is APIControllerBase controller)
            {
                if (!context.ModelState.IsValid)
                {
                    context.Result = controller.StatusCode(400, new APIResponse<string>
                    {
                        Payload = null,
                        Errors = GetErrorMessages(context.ModelState.Values)
                    });
                }
                else if (context.Exception != null)
                {
                    var errors = GetErrorMessagesFromException(context.Exception);
                    context.Result = controller.StatusCode(400, new APIResponse<string>
                    {
                        Payload = null,
                        Errors = errors
                    });
                    context.Exception = null;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is APIControllerBase controller)
            {
                if (!context.ModelState.IsValid)
                {
                    context.Result = controller.StatusCode(400, new APIResponse<string>
                    {
                        Payload = null,
                        Errors = GetErrorMessages(context.ModelState.Values)
                    });
                }
            }
        }

        private IEnumerable<string> GetErrorMessages(ModelStateDictionary.ValueEnumerable values)
        {
            List<string> results = new List<string>();

            foreach (var value in values)
            {
                foreach (var error in value.Errors)
                {
                    results.Add(error.ErrorMessage);
                }

                if (value.Children == null)
                {
                    continue;
                }

                foreach (var child in value.Children)
                {
                    results.AddRange(GetErrorMessages(child));
                }
            }

            return results;
        }

        private IEnumerable<string> GetErrorMessages(ModelStateEntry entry)
        {
            List<string> results = new List<string>();

            foreach (var error in entry.Errors)
            {
                results.Add(error.ErrorMessage);
            }

            if (entry.Children == null)
            {
                return results;
            }

            foreach (var child in entry.Children)
            {
                results.AddRange(GetErrorMessages(child));
            }

            return results;
        }

        private IEnumerable<string> GetErrorMessagesFromException(Exception e)
        {
            List<string> results = new List<string>();

            if (e is RepositoryException repoException)
            {
                foreach (string error in repoException.ErrorMessages)
                {
                    results.Add(error);
                }
            }
            else
            {
                results.Add(e.Message);
            }
            return results;
        }
    }
}
