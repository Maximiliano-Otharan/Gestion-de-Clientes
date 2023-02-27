using CustomersApi.Datos;
using CustomersApi.Repositories;

namespace CustomersApi.CasosDeUso
{
    public interface IUpdateCustomerUserCase
    {
        public Task<CustomerDto?> Execute(CustomerDto customer);
    }

    public class UpdateCustomerUserCase : IUpdateCustomerUserCase
    {
        private readonly System_customers system_customers;

        public UpdateCustomerUserCase(System_customers system_customers)
        {
            this.system_customers = system_customers;
        }

        public async Task<CustomerDto?> Execute(CustomerDto customer)
        {
            var entity = await system_customers.Get(customer.Id);
            if (entity == null) 
            {
                return null;
            }
            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Email = customer.Email;
            entity.Phone = customer.Phone;
            entity.Address = customer.Address;

            await system_customers.Actualizar(entity);
            return entity.ToDto();
        }
    }
}
