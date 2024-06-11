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
using Microsoft.AspNetCore.Cors;

namespace Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomer _customer;
        private readonly ICustomer _customer2;

        public CustomersController(ICustomer customer, ICustomer customer2)
        {
            _customer = customer;
            _customer2 = customer2;
        }

        [EnableCors()]
        [HttpGet]
        [Route("get-customers")]
        public IActionResult GetCustomers()
        {
            try
            {
                return Ok(_customer.GetCustomers());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("get-customers-with-new-one")]
        public IActionResult GetCustomersWithNewOne()
        {
            try
            {
                _customer.GetCustomersWithNewOne();
                return Ok(_customer2.GetCustomers());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
