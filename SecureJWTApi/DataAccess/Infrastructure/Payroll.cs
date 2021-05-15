using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureJWTApi.DataAccess.Infrastructure
{
    public class Payroll
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public double Salary { get; set; }

        public double Bonus { get; set; }
    }
}
