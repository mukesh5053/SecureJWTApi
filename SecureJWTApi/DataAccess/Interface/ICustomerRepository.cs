using SecureJWTApi.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureJWTApi.DataAccess.Interface
{
   public interface ICustomerRepository
    {
        public  Task<IEnumerable< Customer>> GetCustomers();
    }
}
