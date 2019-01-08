using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestMakerFreeWebApp.Data.WFSSDTest
{
    public class StammdatenAbwesenheiten
    {
        [Key]
        [Required]
        public string PERNR { get; set; }
        public string BEGDA { get; set; }
        public string ENDDA { get; set; }
        public string AWART { get; set; }
    }
}
