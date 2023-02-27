using CustomersApi.CasosDeUso;
using CustomersApi.Datos;
using CustomersApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace CustomersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly System_customers system_customers;
        private readonly IUpdateCustomerUserCase updateCustomerUserCase;

        public CustomerController(System_customers system_customers, IUpdateCustomerUserCase updateCustomerUserCase)
        {
            this.system_customers = system_customers;
            this.updateCustomerUserCase = updateCustomerUserCase;
        }

        //api/customer/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getCustomer(long id)
        {
            CustomerEntity result = await this.system_customers.Get(id);

            return new OkObjectResult(result.ToDto());
        }

        //api/customer
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]
        public async Task<IActionResult> getCustomers()
        {
           var result = this.system_customers.Customers.Select(c => c.ToDto()).ToList();

            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> deleteCustomer(long id)
        {
            var result = await this.system_customers.Delete(id);

            return new OkObjectResult(result);
        }

        //api/customer
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> createCustomer(CreateCustomerDto customer)
        {
            CustomerEntity result = await this.system_customers.Add(customer);

            return new CreatedResult($"https://localhost:7164/api/customer/{result.Id}", null);
        }

        //api/customer/{id}
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> updateCustomer(CustomerDto customer)
        {
            CustomerDto? result = await updateCustomerUserCase.Execute(customer);

            if(result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }



    }
}
