using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using backend_bossku._2_Service.Service;
using backend_bossku._2_Service.Service.Interface;

namespace backend_bossku._2_Service
{
    public class ServiceDepedencyProfile
    {
        public static void Register(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IProjectService, ProjectService>();
        }
    }
}
