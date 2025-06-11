using HouseBroker.Infra.Dtos;
using HouseBroker.Infra.Services;
using Microsoft.AspNetCore.Mvc;

namespace HouseBroker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserAuthenticationDtos user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) && string.IsNullOrEmpty(user.Email))
                return BadRequest("Username or Email is required.");

            var token = await _accountService.Login(user);
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Invalid credentials.");

            return Ok(new { Token = token });
        }

        [HttpPost]
        [Route("RegisterRole")]
        public async Task<IActionResult> RegisterRole(RegisterRoleDtos roleDto)
        {
            try
            {
                var result = await _accountService.RegisterRole(roleDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RegisterHouseBroker")]
        public async Task<IActionResult> RegisterBroker(UserAuthenticationDtos user)
        {
            try
            {
                var result = await _accountService.RegisterHomeBroker(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RegisterHouseSeeker")]
        public async Task<IActionResult> RegisterSeeker(UserAuthenticationDtos user)
        {
            try
            {
                var result = await _accountService.RegisterHomeSeeker(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      
    }
}
