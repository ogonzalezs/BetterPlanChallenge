using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betterplan.API.Model
{
    public class Summary
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Balance { get; set; }

        public string Aportes { get; set; }

        public string Moneda { get; set; }

        public DateTime Date { get; set; }
    }
}
