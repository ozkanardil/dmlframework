using System;
using System.Collections.Generic;
using System.Text;

namespace DmlFramework.Infrastructure.Results
{
    public interface IRequestDataResult<out T> : IRequestResult
    {
        T Data { get; }
    }
}
