using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Inibe a opção AutoNumber
        public int ID { get; set; } // chave primária

        // dados do Agente
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")] [RegularExpression("[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+(( |'|-| dos | da | de | e | d')[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+){1,3}", ErrorMessage = "O {0} apenas pode conter letras e espaços em branco. Cada palavra começa em Maiúscula, seguida de minúsculas...")]
        public string Nome { get; set; }

        public string Fotografia { get; set; }

        // local de trabalho do Agente
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
            [RegularExpression("([A-Za-z0-9záéíóúàèìòùâêîôûãõçäëöüïñ -]+)+",
            ErrorMessage = "Escreva um nome válido de uma esquadra...")]
        public string Esquadra { get; set; }

        // Criar uma lista de multas aplicadas pelo Agente
        public virtual ICollection<Multas> ListaMultas { get; set; } //coleção de multas
    }
}