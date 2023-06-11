using System;
using System.Collections.Generic;
using System.Text;

namespace DmlFramework.Infrastructure.Results
{
    public class ErrorRequestResult : RequestResult
    {
        public ErrorRequestResult(string message) : base(false, message)
        {
        }

        public ErrorRequestResult() : base(false)
        {
        }
    }
}
