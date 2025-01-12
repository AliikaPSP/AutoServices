using AutoServices.ApplicationServices.Services;
using AutoServices.CarTest.Macros;
using AutoServices.CarTest.Mock;
using AutoServices.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServices.Data;

namespace AutoServices.CarTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider { get; set; }
        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        public void Dispose()
        {
        }
        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }
        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<ICarsServices, CarsServices>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IHostEnvironment, MockIHostEnvironment>();
            services.AddDbContext<AutoServicesContext>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            RegisterMacros(services);
        }
        private void RegisterMacros(IServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);
            var macros = macroBaseType.Assembly.GetTypes()
                .Where(x => macroBaseType.IsAssignableFrom(x) && !x.IsInterface
                && !x.IsAbstract);

            foreach (var macro in macros)
            {
                services.AddSingleton(macro);
            }
        }
    }
}
