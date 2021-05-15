using Microsoft.EntityFrameworkCore;
using SecureJWTApi.DataAccess.DataContext;
using SecureJWTApi.DataAccess.Infrastructure;
using SecureJWTApi.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureJWTApi.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SecureDBContext dbcontext;

        public CustomerRepository(SecureDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return  await dbcontext.Customer.ToListAsync();
        }
    }
}
