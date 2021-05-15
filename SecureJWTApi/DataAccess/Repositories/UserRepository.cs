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
    public class UserRepository : IUserRepository
    {
        private readonly SecureDBContext context;

        public UserRepository(SecureDBContext context)
        {
            this.context = context;
        }
        public async Task<User> ValidateUser(string username, string password)
        {
           return await context.User.FirstOrDefaultAsync(x => x.UserName.Equals(username) && x.Password.Equals(password));
          
        }
    }
}
