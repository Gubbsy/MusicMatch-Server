using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server
{
    public class APIControllerBase : ControllerBase
    {

        private const string NoRequestGiven = "No Request Given";

        [NonAction]
        protected ObjectResult Ok<T>(T payload) where T : class
        {
            return StatusCode(200, new APIResponse<T>
            {
                Payload = payload
            });
        }

        [NonAction]
        protected ObjectResult Created<T>(T payload) where T : class
        {
            return StatusCode(201, new APIResponse<T>
            {
                Payload = payload
            });
        }

        [NonAction]
        protected ObjectResult Created()
        {
            return StatusCode(201, null);
        }

        [NonAction]
        protected new ObjectResult NoContent()
        {
            return StatusCode(204, null);
        }

        [NonAction]
        protected ObjectResult NoRequest()
        {
            return BadRequest(NoRequestGiven);
        }

        [NonAction]
        protected ObjectResult BadRequest(params string[] errors)
        {
            return StatusCode(400, new APIResponse<string>
            {
                Payload = null,
                Errors = errors
            });
        }

        [NonAction]
        protected ObjectResult NotFound(params string[] errors)
        {
            return StatusCode(404, new APIResponse<string>
            {
                Payload = null,
                Errors = errors
            });
        }

        [NonAction]
        protected ObjectResult Unauthorized(params string[] errors)
        {
            return StatusCode(401, new APIResponse<string>
            {
                Payload = null,
                Errors = errors
            });
        }

        [NonAction]
        protected ObjectResult ServerError(params string[] errors)
        {
            return StatusCode(500, new APIResponse<string>
            {
                Payload = null,
                Errors = errors
            });
        }
    }
}

