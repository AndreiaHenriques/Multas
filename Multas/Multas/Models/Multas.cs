﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; //Adicionado com [Foreignkey]
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Multas
    {
        public int ID { get; set; }
        //Dados da multa
        public string  Infracao { get; set; }

        public string localDaMulta { get; set; }

        public decimal ValorMulta { get; set; }

        public DateTime DataDaMulta { get; set; }

        // **********************************************
        // especificação das chaves forasteiras
        // **********************************************

        //FK para o Agente
        //em SQL:
        //...Foreignkey NomeDaVarQueÉFK references NomeDaTabela(nomeDaPK)

        [ForeignKey("Agente")]
        public int AgenteFk { get; set; }
        public virtual Agentes Agente { get; set; }

        //Fk para Condutor
        [ForeignKey("Condutor")]
        public int CondutorFk { get; set; }
        public virtual Condutores Condutor { get; set; }


        //Fk para Viatura
        [ForeignKey("Viatura")]
        public int ViaturaFk { get; set; }
        public virtual Viaturas Viatura { get; set; }



    }
}