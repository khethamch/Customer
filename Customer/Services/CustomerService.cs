using Customer.Data;
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
    //ORM


    //What is an ORM?
    //    An ORM, or Object-Relational Mapping, is a programming technique used to convert data between incompatible type systems, typically between object-oriented programming languages and relational databases.In simpler terms, it's a way to interact with a database using an object-oriented approach, where database tables are represented as classes, rows as objects, and relationships between tables as object references.

    //ORM libraries provide developers with an abstraction layer that allows them to work with databases using high-level programming constructs, such as classes, objects, and methods, rather than dealing directly with SQL queries and database tables.This abstraction simplifies database interactions and makes it easier to develop and maintain database-driven applications.Popular ORMs include SQLAlchemy for Python, Hibernate for Java, and Entity Framework for .NET.
    //    Types of ORM?

    //ORMs come in various types based on their architecture, implementation, and features. Here are some common types:

    //1. **Full-Featured ORM**: These are comprehensive ORM libraries that provide a wide range of features for mapping objects to database tables, managing database connections, executing queries, and handling relationships between objects.Examples include SQLAlchemy for Python and Hibernate for Java.

    //2. **Lightweight ORM**: Lightweight ORMs are simpler and more focused on basic object-relational mapping functionality, often with fewer features compared to full-featured ORMs. They are suitable for projects with simpler data models and requirements.Examples include Peewee for Python and Dapper for .NET.

    //3. **Micro ORM**: Micro ORMs are minimalist ORM libraries that aim to provide lightweight and fast object-relational mapping without the complexity and overhead of full-featured ORMs. They typically focus on mapping individual queries to objects without providing extensive features like automatic schema generation or complex relationship management.Examples include Dapper for .NET and MyBatis for Java and .NET.

    //4. **Active Record Pattern**: In this pattern, each database table is represented by a class, and instances of these classes correspond to rows in the table.The ORM handles the mapping between objects and database records as well as the CRUD operations.Examples include Ruby on Rails' ActiveRecord and Django's ORM.

    //5. **Data Mapper Pattern**: In contrast to the Active Record pattern, the Data Mapper pattern separates the database logic from the domain logic. Objects are independent of the database, and a mapper handles the communication between objects and the database.Examples include Doctrine for PHP and Entity Framework Core for .NET.
    //    Each type of ORM has its advantages and is suitable for different types of projects and development preferences.Choosing the right ORM depends on factors such as the complexity of the project, performance requirements, programming language, and personal preferences of the development team.


    //How do I choose an ORM?

    //Choosing the right ORM for your project involves considering several factors to ensure it aligns with your project requirements, development team's skills, and long-term goals. Here's a step-by-step guide to help you choose an ORM:

    //1. **Understand Your Project Requirements**: Determine the specific requirements of your project, including the complexity of your data model, performance needs, scalability requirements, and any unique features you may need from an ORM.

    //2. **Evaluate Language Compatibility**: Ensure the ORM supports the programming language you're using for your project. Most popular ORMs support multiple programming languages, but some are specific to certain languages.

    //3. **Assess Feature Set**: Evaluate the features offered by each ORM and compare them against your project requirements. Consider features such as CRUD operations, query building capabilities, support for transactions, caching mechanisms, and support for advanced database features like stored procedures and triggers.

    //4. **Consider Performance**: Performance is critical, especially for high-traffic applications or those dealing with large datasets. Look for benchmarks or performance comparisons between different ORM options to gauge their efficiency. Keep in mind that the performance of an ORM can also depend on how it's used and configured in your application.

    //5. **Check Community and Support**: Consider the size and activity of the community around the ORM.A vibrant community usually means better support, documentation, and a wider range of third-party libraries and extensions. Look for active forums, mailing lists, documentation, and GitHub repositories.

    //6. **Evaluate Learning Curve**: Consider the learning curve associated with each ORM, especially if your team is not already familiar with it.Some ORMs are simpler and easier to learn, while others may have a steeper learning curve but offer more advanced features.

    //7. **Review Documentation and Tutorials**: Look for comprehensive documentation, tutorials, and examples for each ORM to assess how easy it is to get started and how well-documented the features are.

    //8. **Consider Long-Term Maintenance**: Think about the long-term maintenance and support of the ORM.Choose an ORM that is actively maintained, regularly updated, and has a roadmap for future development to ensure compatibility with future versions of your programming language and database.

    //9. **Experiment and Prototype**: If possible, experiment with a few different ORMs by building small prototypes or sample projects to get a feel for how they work in practice and how well they meet your project requirements.

    //10. **Seek Recommendations**: Ask for recommendations from peers, colleagues, or online communities who have experience using different ORMs.Their insights and experiences can help you make a more informed decision.

    //By carefully considering these factors and evaluating your options, you can choose an ORM that best fits your project needs and helps you build scalable, maintainable, and efficient database-driven applications.
   
    public class CustomerService : ICustomer
    {
        private readonly IDatabaseHandler _database;
        public CustomerService(IDatabaseHandler database)
        {
            _database = database;
        }
        public async Task CreateCustomer(CustomerModel customer)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Name", customer.Name);
            dynamicParameters.Add("Surname", customer.Surname);
            dynamicParameters.Add("Contact", customer.Contact);
            dynamicParameters.Add("Email", customer.Email);

            var connection = _database.GetDbConnection();

            connection.Open();
            await connection.QueryAsync(@"INSERT INTO [dbo].[Customer]
           ([Name]
           ,[Surname]
           ,[Contact]
           ,[Email])
     VALUES
           (@Name
           ,@Surname
           ,@Contact
           ,@Email)", dynamicParameters, commandTimeout: 600).ConfigureAwait(false);
            connection.Close();
        }

        public async Task DeleteCustomer(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Id", id);

            var connection = _database.GetDbConnection();

            connection.Open();
            await connection.QueryAsync(@"DELETE FROM [dbo].[Customer] WHERE [Id] = @Id", dynamicParameters, commandTimeout: 600)
                .ConfigureAwait(false);
            connection.Close();
        }

        public async Task<CustomerModel> GetCustomer(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Id", id);

            var connection = _database.GetDbConnection();

            connection.Open();
            CustomerModel customer = await connection.QueryFirstAsync<CustomerModel>
                (@"SELECT [Id]
      ,[Name]
      ,[Surname]
      ,[Contact]
      ,[Email]
  FROM [dbo].[Customer]
  WHERE [Id] = @Id", dynamicParameters, commandTimeout: 600)
                .ConfigureAwait(false);
            connection.Close();

            return customer;
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomers()
        {
            var connection = _database.GetDbConnection();

            connection.Open();
            IEnumerable<CustomerModel> customers = await connection.QueryAsync<CustomerModel>
                (@"SELECT [Id]
      ,[Name]
      ,[Surname]
      ,[Contact]
      ,[Email]
  FROM [dbo].[Customer]", commandTimeout: 600)
                .ConfigureAwait(false);
            connection.Close();

            return customers;
        }

        public async Task UpdateCustomer(CustomerModel customer)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Id", customer.Id);
            dynamicParameters.Add("Name", customer.Name);
            dynamicParameters.Add("Surname", customer.Surname);
            dynamicParameters.Add("Contact", customer.Contact);
            dynamicParameters.Add("Email", customer.Email);

            var connection = _database.GetDbConnection();

            connection.Open();
            await connection.QueryAsync(@"UPDATE [dbo].[Customer]
   SET [Name] = @Name
      ,[Surname] = @Surname
      ,[Contact] = @Contact
      ,[Email] = @Email
 WHERE [Id] = @Id", dynamicParameters, commandTimeout: 600).ConfigureAwait(false);
            connection.Close();
        }

        public async Task UpdateCustomerName(int id, string name)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Id", id);
            dynamicParameters.Add("Name", name);

            var connection = _database.GetDbConnection();

            connection.Open();
            await connection.QueryAsync(@"UPDATE [dbo].[Customer]
   SET [Name] = @Name
 WHERE [Id] = @Id", dynamicParameters, commandTimeout: 600).ConfigureAwait(false);
            connection.Close();
        }
    }
}
