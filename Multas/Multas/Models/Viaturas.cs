using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Viaturas
    {
        //Criar o construtor
        public Viaturas()
        {
            ListaMultas = new HashSet<Multas>(); //carrega as multas
        }
        public int ID { get; set; } //chave primária

        //Dados da viatura
        public string Matricula { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Cor { get; set; }

        //Descrição do dono das viaturas

        public string NomeDono { get; set; }

        public string MoradaDono { get; set; }

        public string CodPostalDono { get; set; }

        //Criar uma lista de multas associadas às Viaturas
        public virtual ICollection<Multas> ListaMultas { get; set; } //coleção de multas


    }
}