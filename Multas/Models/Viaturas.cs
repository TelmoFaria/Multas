using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Viaturas {

        public Viaturas() {
            ListaDeMultas = new HashSet<Multas>();
        }
        
        [Key]
        public int ID{ get; set; }

        public string Matricula { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Cor { get; set; }

        //Dados dono 1 viatura

        public string NomeDono { get; set; }

        public string MoradaDono { get; set; }

        public string CodPostDono { get; set; }

        //referencia às multas que um condutor recebe numa Viatura

        public virtual ICollection<Multas> ListaDeMultas { get; set; }




    }
}