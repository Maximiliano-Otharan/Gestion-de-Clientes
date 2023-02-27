using CustomersApi.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomersApi.Repositories
{
    public class System_customers : DbContext
    {
        public System_customers(DbContextOptions<System_customers> options) 
            : base(options)
        { 
            
        }   

        public DbSet<CustomerEntity> Customers { get; set; }

        public async Task<CustomerEntity?> Get(long id)
        {
            return await Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Delete(long id)
        {
            CustomerEntity entity = await Get(id);
            Customers.Remove(entity);
            SaveChanges();
            return true;
        }

        public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
        {
            CustomerEntity entity = new CustomerEntity()
            {
                Id = null,
                Address = customerDto.Address,
                Email = customerDto.Email,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Phone = customerDto.Phone
            };
            EntityEntry<CustomerEntity> response = await Customers.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("no se a podido guardar"));
        }

        public async Task<bool> Actualizar(CustomerEntity entity)
        {
            Customers.Update(entity);
            await SaveChangesAsync();

            return true;
        }
    }

    public class CustomerEntity
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public CustomerDto ToDto()
        {
            return new CustomerDto()
            {

                Address = Address,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                Id = Id ?? throw new Exception("El id no puede ser NULL")
            };
        }
    }
}
