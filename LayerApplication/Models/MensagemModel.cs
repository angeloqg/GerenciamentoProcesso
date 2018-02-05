using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayerApplication.Models
{
    public class MensagemModel
    {
        public List<DadosModel> Dados { get; set; }
        public string Mensagem { get; set; }
        public string ValorTotal { get; set; }
        public string ValorMedio { get; set; }
        public int NumeroProcessos { get; set; }
    }
}