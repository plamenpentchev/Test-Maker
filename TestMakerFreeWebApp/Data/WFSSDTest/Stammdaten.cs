using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestMakerFreeWebApp.Data.WFSSDTest
{
    public class Stammdaten
    {
        [Key]
        [Required]
        public string PERNR { get; set; }
        public string WERKS { get; set; }
        public string NAME1 { get; set; }
        public string BTRTL { get; set; }
        public string BTEXT { get; set; }
        public string NACHN { get; set; }
        public string VORNA { get; set; }
        public string TITEL { get; set; }
        public string ANREX { get; set; }
        public string GBORT { get; set; }
        public string GBLND { get; set; }
    }
}
