using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestMakerFreeWebApp.Configs;

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        public Bank CentralBank { get; protected set; }
        public BankController(Bank centralbank )
        {
            CentralBank = centralbank;
        }


        [HttpGet("[action]")]
        public Bank GetCentralBank() => CentralBank;
    }
}