using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyPiggyBank.Web.Controller
{
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> ReturnBadRequestIfThrowError<TResult>(Func<Task<TResult>> innerFunc)
        {
            try
            {
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
    }
}
