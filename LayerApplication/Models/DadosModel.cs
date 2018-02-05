using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayerApplication.Models
{
    public class DadosModel
    {
        public string Codigo { get; set; }
        public string Empresa { get; set; }
        public string EstadoEmpresa { get; set; }
        public string Cnpj { get; set; }
        public string Tipo { get; set; }
        public string Situacao { get; set; }
        public string Estado { get; set; }
        public string DataInicio { get; set; }
        public string Valor { get; set; }
        public string Processo { get; set; }
    }
}