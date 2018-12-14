using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TestMakerFreeWebApp.Configs;

namespace TestMakerFreeWebApp.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddBank(this IServiceCollection service, IConfiguration config)
        {
            var section = config.GetSection("Bank");
            var settings = new BankSettings();
            new ConfigureFromConfigurationOptions<BankSettings>(section).Configure(settings);
            return service.AddSingleton(new Bank(settings));
        }
    }
}
