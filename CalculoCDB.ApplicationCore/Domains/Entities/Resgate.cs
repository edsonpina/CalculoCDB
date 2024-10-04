using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoCDB.ApplicationCore.Domains.Entities
{
    public class Resgate
    {
        public string VF { get; set; }
        public string VI { get; set; }
        public string CDI { get; set; }
        public string TB { get; set; }
        public string Imposto { get; set; }
        public string TotalImposto { get; set; }
        public string TotalLiquido { get; set; }
    }
}
