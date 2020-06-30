using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol.Account.Requests;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Web.Controller;

namespace MyPiggyBank.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountsService _accountService;
        private readonly IJwtService _jwtService;

        public AccountController(IAccountsService accountService, IJwtService jwtService)
        {
            _accountService = accountService;
            _jwtService = jwtService;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest input) 
            => await ReturnBadRequestIfThrowError(async() => await _accountService.SaveAccount(input));

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginRequest input)
            => await ReturnBadRequestIfThrowError(async () =>
            {
                var accInfo = await _accountService.Authenticate(input);
                return _jwtService.GenerateToken(accInfo);;
            });
    }
}