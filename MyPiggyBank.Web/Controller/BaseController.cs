using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace MyPiggyBank.Web.Controller
{
    public abstract class BaseController : ControllerBase
    {
        protected Guid UserId
        {
            get
            {
                Guid.TryParse(GetClaim(JwtRegisteredClaimNames.Sub), out var result);
                return result;
            }
        }

        protected async Task<IActionResult> ReturnBadRequestIfThrowError<TResult>(Func<Task<TResult>> innerFunc)
        {
            try
            {
                var expr = Expression.Lambda<Func<Task<TResult>>>(Expression.Call(innerFunc.Method));
                var result = await innerFunc.Invoke();
                return Ok(result);
            }
            catch (TargetInvocationException)
            {
                return BadRequest($"Occured unexpected error. Contact with admin.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        protected async Task<IActionResult> ReturnBadRequestIfThrowError(Func<Task> innerFunc)
        {
            try
            {
                await innerFunc.Invoke();
                return Ok();
            }
            catch (TargetInvocationException)
            {
                return BadRequest($"Occured unexpected error. Contact with admin.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        protected IActionResult ReturnBadRequestIfThrowError<TResult>(Func<TResult> innerFunc)
        {
            var @task = Task.Run(innerFunc);
            Func<Task<TResult>> execution = () => @task;
            return ReturnBadRequestIfThrowError(execution).Result;
        }

        private string GetClaim(string registeredClaims)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                return identity.Claims
                               .FirstOrDefault(c => c.Properties.Any(p => p.Value == registeredClaims))
                               ?.Value;
            }

            return null;
        }
    }
}
