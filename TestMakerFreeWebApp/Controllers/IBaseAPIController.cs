using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Data;
using Newtonsoft.Json;

namespace TestMakerFreeWebApp.Controllers
{
    interface IBaseAPIController
    {
        ApplicationDbContext DBContext { get; }
        JsonSerializerSettings JsonSettings { get; }
    }
}
