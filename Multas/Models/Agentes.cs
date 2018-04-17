using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Agentes {

        public Agentes()
        {
            ListaDeMultas = new HashSet<Multas>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        [RegularExpression("[A-ZÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÕÃÇÄËÏÖÜ][a-záéíóúàèìòùâêîôûãõçäëïöü]+(( |'|-| dos | de | da | e | do | das | d' |)[A-ZÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÕÃÇÄËÏÖÜ][a-záéíóúàèìòùâêîôûãõçäëïöü]+){1,3}", 
            ErrorMessage ="O {0} apemas pode conter letras e espaços em branco. Cada palavra começa com maiuscula seguida de minusculas...")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        //[RegularExpression("[A-ZÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÕÃÇÄËÏÖÜ][a-záéíóúàèìòùâêîôûãõçäëïöü]+(( |-| do | da | dos | das | de |)[A-ZÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÕÃÇÄËÏÖÜ][a-záéíóúàèìòùâêîôûãõçäëïöü]+){1,3}", ErrorMessage = "A {0} não é válida")]
        public string Esquadra { get; set; }

        public string Fotografia { get; set; }

        //referencia às multas que um agente emite

        public virtual ICollection<Multas> ListaDeMultas { get; set; }
        
    }
}