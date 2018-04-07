using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Agentes
    {
        // Criar o construtor
        public Agentes()
        {
            ListaMultas = new HashSet<Multas>(); // carrega as multas
        }

        [Key]
        public int ID { get; set; } // chave primária

        // dados do Agente
        [Required(ErrorMessage ="O {0} é de preenchimento obrigatório")]
        [RegularExpression("A-ZÂÁÓÉÍ][a-záéíóúàèìòùâêîôûãõçäëïöüñ]+(( | e | de | da | das | do | d' | - |)A-ZÂÁÓÉÍ][a-záéíóúàèìòùâêîôûãõçäëïöüñ]+) {1,3} ",
            ErrorMessage ="O {0} só aceita letras. Cada nome deve começar por letra maiuscula seguido de letras minusculas")]
        [StringLength(40, ErrorMessage ="O {0} só aceita {1} caracteres.")]

        public string Nome { get; set; }

        [Required(ErrorMessage ="A {0} é de preenchimento obrigatório")]
        public string Fotografia { get; set; }

        // local de trabalho do Agente
        [Required(ErrorMessage ="A {0} é de preenchimento obrigatório")]
        [RegularExpression ("[A-Za-z0-9ç -]+", 
            ErrorMessage ="Escreva um nome v] válido de uma esquadra...")]
        public string Esquadra { get; set; }

        // Criar uma lista de multas aplicadas pelo Agente
        public virtual ICollection<Multas> ListaMultas { get; set; } //coleção de multas
    }
}