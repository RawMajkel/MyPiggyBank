using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Communication.Account.Requests;
using MyPiggyBank.Core.Services.Account.Interface;

namespace MyPiggyBank.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtService _jwtService;

        public AccountController(IAccountService accountService, IJwtService jwtService)
        {
            _accountService = accountService;
            _jwtService = jwtService;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest input)
        {
            try
            {
                await _accountService.SaveAccount(input);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult> Login([FromBody] LoginRequest input)
        {
            try
            {
                var accInfo = await _accountService.Authenticate(input);
                var token = _jwtService.GenerateToken(accInfo);

                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}