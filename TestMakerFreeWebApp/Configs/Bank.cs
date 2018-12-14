using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Complete example at
//    https://rimdev.io/strongly-typed-configuration-settings-in-asp-net-core-part-ii/

namespace TestMakerFreeWebApp.Configs
{
    public class Bank
    {
        public Bank(BankSettings config)
        {
            if (null == config) throw new ArgumentException(nameof(config));
            this.AccountNumber = config.AccountNumber;
            this.Name = config.Name;
        }

        public Guid AccountNumber { get; protected set; }
        public string Name { get; protected set; }
    }

    public class BankSettings
    {
        public Guid AccountNumber { get; set; }
        public string Name { get; set; }

    }

   
}
