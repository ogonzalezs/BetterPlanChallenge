using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betterplan.API.Model
{
    public class GoalDetail
    {
        public int UserId { get; set; }

        public int GoalId { get; set; }

        public string Name { get; set; }

        public string Titulo { get; set; }

        public string Years { get; set; }

        public string InversionInicial { get; set; }

        public string ContribucionMensual { get; set; }

        public string Objetivo { get; set; }

        public string Entidad { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Aportes { get; set; }

        public string Porcentaje { get; set; }
    }
}
