using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace Customer.Controllers
{
    public class CodesController : Controller
    {
        //1. **1xx Informational**:
        //   - 100 Continue
        //   - 101 Switching Protocols
        //   - 102 Processing(WebDAV; RFC 2518)

        //2. **2xx Success**:
        //   - 200 OK
        //   - 201 Created
        //   - 202 Accepted
        //   - 204 No Content
        //   - 206 Partial Content

        //3. **3xx Redirection**:
        //   - 300 Multiple Choices
        //   - 301 Moved Permanently
        //   - 302 Found
        //   - 304 Not Modified
        //   - 307 Temporary Redirect
        //   - 308 Permanent Redirect

        //4. **4xx Client Error**:
        //   - 400 Bad Request
        //   - 401 Unauthorized
        //   - 403 Forbidden
        //   - 404 Not Found
        //   - 405 Method Not Allowed
        //   - 409 Conflict
        //   - 429 Too Many Requests

        //5. **5xx Server Error**:
        //   - 500 Internal Server Error
        //   - 501 Not Implemented
        //   - 502 Bad Gateway
        //   - 503 Service Unavailable
        //   - 504 Gateway Timeout
        //   - 505 HTTP Version Not Supported

        //200
        [HttpGet("GetEmployees")]
        public IActionResult GetEmployees()
        {
            throw new NotImplementedException();    
            return Ok();
        }

        //300
        [HttpGet]
        [Route("OldEndpoint")]
        public IActionResult GetFromOldEndpoint()
        {
            // The URL of the new location for the resource
            string newUrl = "https://localhost:7142//api/ErrorCodes/NewEndpoint";
            return Redirect(newUrl);


        }

        [HttpGet]
        [Route("NewEndpoint")]
        public IActionResult GetFromNewEndpoint()
        {
            // Handle the request as usual
            return Ok("This is the new endpoint.");
        }

        //400
        [HttpGet]
        public IActionResult SecureResource()
        {
            // Your authentication logic here
            // Assuming this is the result of your auth check
            bool isAuthenticated = false;
            if (!isAuthenticated)
            {
                return Unauthorized(); // Returns a 401 Unauthorized response
            }
            // Proceed with normal action if authenticated
            return Ok("Authenticated and Authorized Access.");
        }

        //500
        [HttpGet]
        public IActionResult GetEmployee()
        {
            try
            {
                // Your logic here
                int x = 10, y = 0;
                int z = x / y; //This statement will throw exception
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception details

                var customResponse = new
                {
                    Code = 500,
                    Message = "Internal Server Error",
                    // Do not expose the actual error to the client
                    ErrorMessage = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, customResponse);
            }
        }
    }
}
