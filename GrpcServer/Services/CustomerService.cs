using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService : Customers.CustomersBase
    {
        private readonly ILogger<CustomersService> __logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            __logger = logger; 
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if(request.UserID == 1)
            {
                output.FirstName = "Bel";
                output.LastName = "Corey";
            } else if (request.UserID == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe"; 
            } else
            {
                output.FirstName = "Greg";
                output.LastName = "Thomas"; 
            }

            return Task.FromResult(output);


        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {

            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                   FirstName = "Bel",
                   LastName = "Corey",
                   EmailAdress = "bel@gmail.com",
                   Age = 20,
                   IsAlive = true
                },
                new CustomerModel
                {
                   FirstName = "Sue",
                   LastName = "Storm",
                   EmailAdress = "sue@stormy.com",
                   Age = 40,
                   IsAlive = false
                },
                new CustomerModel
                {
                   FirstName = "Bilbo",
                   LastName = "Alpert",
                   EmailAdress = "bilbo@alpert.com",
                   Age = 87,
                   IsAlive = false
                },
            };

            foreach(var cust in customers)
            {
                await responseStream.WriteAsync(cust);
            }

        }



    }
}
