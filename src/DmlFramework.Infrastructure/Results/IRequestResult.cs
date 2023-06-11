using System;
using System.Collections.Generic;
using System.Text;

namespace DmlFramework.Infrastructure.Results
{
    public interface IRequestResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
