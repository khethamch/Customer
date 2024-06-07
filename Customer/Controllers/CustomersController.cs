using Customer.Models;
using Customer.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System;

namespace Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //Http verbs
        //Get, Post, Put, Patch, Delete

        //HTTP(Hypertext Transfer Protocol) defines several request methods, also known as HTTP verbs, that indicate the desired action to be performed on a resource.Here are some of the most common HTTP verbs:

        //1. **GET**: Requests data from a specified resource. It should only retrieve data and not modify anything on the server.

        //2. **POST**: Submits data to be processed to a specified resource.It's commonly used for submitting form data or uploading files.

        //3. **PUT**: Updates a specified resource with the provided data. It replaces the existing resource or creates a new one if it doesn't exist.

        //4. **DELETE**: Deletes the specified resource.

        //5. **PATCH**: Applies partial modifications to a resource.It's typically used when you want to apply partial updates to a resource.

        //6. **HEAD**: Requests the headers of a resource, similar to GET, but without the response body.It's often used to check for the existence or metadata of a resource.

        //7. **OPTIONS**: Returns the HTTP methods supported by the server for a specified URL.It's commonly used in CORS (Cross-Origin Resource Sharing) requests to determine which HTTP methods and headers are allowed by the server.

        //8. **TRACE**: Echoes back the received request to the client, which is primarily used for diagnostic purposes.

        //9. **CONNECT**: Establishes a tunnel to the server identified by the target resource.

        private readonly ICustomer _customer;

        public CustomersController(ICustomer customer)
        {
            _customer = customer;
        }

        [HttpDelete]
        [Route("delete-customer")]
        public async Task<IActionResult> DeleteCustomerName([FromQuery] int id)
        {
            try
            {
                await _customer.DeleteCustomer(id);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        [Route("update-customer-name/{id}/{name}")]
        public async Task<IActionResult> UpdateCustomerName([FromRoute] int id, string name)
        {
            try
            {
                await _customer.UpdateCustomerName(id, name);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("update-customer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerModel customer)
        {
            try
            {
                await _customer.UpdateCustomer(customer);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("create-customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerModel customer)
        {
            try
            {
                await _customer.CreateCustomer(customer);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("get-customer")]
        public async Task<IActionResult> GetCustomer([FromQuery] int id)
        {
            try
            {
                return Ok(await _customer.GetCustomer(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("get-customers")]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                return Ok(await _customer.GetCustomers());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpHead]
        [Route("get-resourceInfo")]
        public IActionResult GetResourceHeaders()
        {
            var resourceMetadata = new
            {
                LastModified = DateTime.UtcNow,
                CustomHeader = "ABCDZYZ"
            };
            Response.Headers.Append("Last-Modified", resourceMetadata.LastModified.ToString("R")); // RFC1123 format
            Response.Headers.Append("CustomHeader", resourceMetadata.CustomHeader);
            return Ok();
        }

        [HttpOptions]
        [Route("get-options")]
        public IActionResult Options()
        {
            Response.Headers.Append("Allow", "GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
