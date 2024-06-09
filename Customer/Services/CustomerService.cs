
using Customer.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.VisualBasic;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using System;
using static Dapper.SqlMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Customer.Services
{
    
    public class CustomerService : ICustomer
    {
        List<CustomerModel> customers = new List<CustomerModel>();
        public CustomerService()
        {
            customers = new List<CustomerModel>()
            {
                new CustomerModel() { Id = 1, Name = "Beyonce", Surname = "Knowles", Contact = "+1587456254", Email = "queenb@knowles.com"},
                new CustomerModel() { Id = 2, Name = "Jay-Z", Surname = "Cater", Contact = "+15874578554", Email = "j@cater.com"},
                new CustomerModel() { Id = 3, Name = "Busta", Surname = "Rhymes", Contact = "+1854669625", Email = "busta@rhymes.com"},
            };
        }
        public List<CustomerModel> GetCustomers()
        {          
            return customers;
        }

        public List<CustomerModel> GetCustomersWithNewOne()
        {
            Random random = new Random();
            customers.Add(new CustomerModel() { Id = random.Next(), Name = "Khetha", Surname = "Mchunu", Contact = "+2785451845", Email = "k@mh.com" });
            return customers;
        }
    }
}
