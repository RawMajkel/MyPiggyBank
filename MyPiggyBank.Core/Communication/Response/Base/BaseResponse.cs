using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Communication.Response.Base
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
