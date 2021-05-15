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
    public class PayrollRepository : IPayrollRepository
    {
        private readonly SecureDBContext secureDBContext;

        public PayrollRepository(SecureDBContext secureDBContext)
        {
            this.secureDBContext = secureDBContext;
        }

        public async  Task<Payroll> GetCustomerPayroll(int customerId)
        {
            return await secureDBContext.Payrolls.FirstOrDefaultAsync(x => x.CustomerId == customerId);            
        }
    }
}
