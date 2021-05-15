using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecureJWTApi.DataAccess.Infrastructure;
using SecureJWTApi.DataAccess.Interface;
using SecureJWTApi.Infrastructure;
using SecureJWTApi.Security.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecureJWTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> logger;
        private readonly ICustomerRepository customerRepository = null;
        private readonly IPayrollRepository payrollRepository = null;
        private readonly IUserRepository userRepository = null;
        private readonly IJwtAuthManager jwtAuthManager;

        public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customerRepository, IPayrollRepository payrollRepository, IUserRepository userRepository, IJwtAuthManager jwtAuthManager)
        {
            this.logger = logger;
            this.customerRepository = customerRepository;
            this.payrollRepository = payrollRepository;
            this.userRepository = userRepository;
            this.jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public  async Task<ActionResult> Login([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var result = await userRepository.ValidateUser(user.UserName, user.Password);

            if (result != null)
            {
                var role = result.Role;
                var claims = new[]
                {
                new Claim(ClaimTypes.Name,result.UserName),
                new Claim(ClaimTypes.Role, role)
            };

                var jwtResult = jwtAuthManager.GenerateTokens(result.UserName, claims, DateTime.Now);
                logger.LogInformation($"User [{result.UserName}] logged in the system.");
                return Ok(new LoginResult
                {
                    UserName = result.UserName,
                    Role = role,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            else
            {
                return Unauthorized("Login failed");
            }

        }

        [HttpPost("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            var userName = User.Identity?.Name;
            jwtAuthManager.RemoveRefreshTokenByUserName(userName);
            logger.LogInformation($"User [{userName}] logged out the system.");
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustomer()
        {
            var result = await customerRepository.GetCustomers();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

       [Route("payroll/{id}")]
       [HttpGet]
       [Authorize(Roles = UserRoles.Manager)]
        public async Task<ActionResult<Payroll>> GetCustomerPayroll([FromRoute]int id)
        {
            var result = await payrollRepository.GetCustomerPayroll(id);

            if (result != null)
            {
                return Ok(result);
            
            }
            else
              {
                return NotFound("Customer not found...");
            }

        }
         
    }
}
