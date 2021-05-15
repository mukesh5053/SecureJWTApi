using SecureJWTApi.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureJWTApi.DataAccess.Interface
{
  public  interface IPayrollRepository
    {

        public Task<Payroll> GetCustomerPayroll(int customerId);
    }
}
