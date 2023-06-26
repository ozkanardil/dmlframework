using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DmlFramework.Infrastructure.Security.JwtToken;
using DmlFramework.Infrastructure.Errors.Middleware;
using DmlFramework.Infrastructure.CustomExceptionFilter;
using DmlFramework.Infrastructure.LogEntries;
using FluentValidation.AspNetCore;
using DmlFramework.Infrastructure.Errors.Interceptor;

namespace DmlFramework.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<ExceptionMiddleware>();

            services.AddTransient<LogFilter>();
            services.AddTransient<ExceptionFilter>();

            services.AddTransient<IValidatorInterceptor, UseCustomErrorModelInterceptor>();

            return services;

        }
    }
}
