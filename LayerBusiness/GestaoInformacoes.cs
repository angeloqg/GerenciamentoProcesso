using LayerData;
using LayerDomain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayerBusiness
{
    public class GestaoInformacoes
    {
        string arquivo = String.Empty;
        public GestaoInformacoes()
        {
        }

        public GestaoInformacoes(string arquivo)
        {
            this.arquivo = arquivo;
        }

        public Processos RetornaDadosProcesso(string id)
        {
            Processos valor = new Processos();

            try
            {
                valor = ListaProcessos(id, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty).First();
            }catch
            {
                // -- Sem ação
            }

            return valor;
        }


        public List<Processos> ListaProcessos(string id, string tipo, string situacao, string empresa, string oprBusca,string valorProcesso, string optBuscaData, string dia, string mes, string ano, string estado )
        {

            var query = new GerenciaDados(arquivo).listarXml();

            if (!String.IsNullOrEmpty(id))
            {
                int valId = 0;

                if(int.TryParse(id, out valId))
                    query = query.AsEnumerable().Where(i => i.Codigo == valId).ToList();
            }

            // -- Filtro pela empresa
            if (!String.IsNullOrEmpty(empresa))
            {
                empresa = empresa.Trim();
                empresa = empresa.ToLower();
                query = query.AsEnumerable().Where(i => i.Empresa.ToLower().Equals(empresa)).ToList();
            }

            // // -- Filtro pela situação
             if (!String.IsNullOrEmpty(situacao))
             {
                 situacao = situacao.Trim();
                 situacao = situacao.ToLower();
                 query = query.AsEnumerable().Where(i => i.Situacao.ToLower().Equals(situacao)).ToList();
             }
            
            // -- Filtro pelo tipo
            if (!String.IsNullOrEmpty(tipo))
            {
                tipo = tipo.Trim();
                tipo = tipo.ToLower();
                query = query.AsEnumerable().Where(i => i.Tipo.ToLower().Equals(tipo)).ToList();
            }
            
            // -- Filtro pelo estado
            if (!String.IsNullOrEmpty(estado))
            {
                estado = estado.Trim();
                estado = estado.ToLower();
                query = query.AsEnumerable().Where(i => i.Estado.ToLower().Equals(estado)).ToList();
            }

            // -- Filtro pelo valor do processo
            if(!String.IsNullOrEmpty(valorProcesso))
            {
                oprBusca = oprBusca.Trim();
                valorProcesso = valorProcesso.Trim();
                valorProcesso = valorProcesso.Replace(".", "");
                Decimal valor = 0;

                bool verifica = Decimal.TryParse(valorProcesso, out valor);

                if (!String.IsNullOrEmpty(oprBusca))
                {

                    switch (oprBusca)
                    {
                        case "=":
                            if (verifica)
                                query = query.AsEnumerable().Where(i => i.Valor == valor).ToList();
                            break;
                        case ">":
                            if (verifica)
                                query = query.AsEnumerable().Where(i => i.Valor > valor).ToList();
                            break;

                        case "<":
                            if (verifica)
                                query = query.AsEnumerable().Where(i => i.Valor < valor).ToList();
                            break;

                        case ">=":
                            if (verifica)
                                query = query.AsEnumerable().Where(i => i.Valor >= valor).ToList();
                            break;

                        case "<=":
                            if (verifica)
                                query = query.AsEnumerable().Where(i => i.Valor <= valor).ToList();
                            break;

                        case "<>":
                            if (verifica)
                                query = query.AsEnumerable().Where(i => i.Valor != valor).ToList();
                            break;
                    }
                }else
                {
                    query = query.AsEnumerable().Where(i => i.Valor == valor).ToList();
                }
            }

            if (!String.IsNullOrEmpty(dia) || !String.IsNullOrEmpty(mes) || !String.IsNullOrEmpty(ano))
            {
                int valDia = 0;
                int valMes = 0;
                int valAno = 0;

                string data = String.Empty;

                if(int.TryParse(dia, out valDia))
                    valDia = (valDia != 0) ? valDia : 0;

                if (int.TryParse(mes, out valMes))
                    valMes = (valMes != 0) ? valMes : 0;

                if (int.TryParse(ano, out valAno))
                    valAno = (valAno != 0) ? valAno : 0;

                if (String.IsNullOrEmpty(optBuscaData))
                {
                    if(valDia > 0)
                        query = query.AsEnumerable().Where(i => i.DataInicio.Day == valDia).ToList();

                    if (valMes > 0)
                        query = query.AsEnumerable().Where(i => i.DataInicio.Month == valMes).ToList();

                    if(valAno > 0)
                        query = query.AsEnumerable().Where(i => i.DataInicio.Year == valAno).ToList();
                }else
                {
                    switch (optBuscaData)
                    {
                        case "=":
                            if (valDia > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Day == valDia).ToList();

                            if (valMes > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Month == valMes).ToList();

                            if (valAno > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Year == valAno).ToList();
                            break;

                        case ">":
                            if (valDia > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Day > valDia).ToList();

                            if (valMes > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Month > valMes).ToList();

                            if (valAno > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Year > valAno).ToList();
                            break;

                        case "<":
                            if (valDia > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Day < valDia).ToList();

                            if (valMes > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Month < valMes).ToList();

                            if (valAno > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Year < valAno).ToList();
                            break;

                        case ">=":
                            if (valDia > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Day >= valDia).ToList();

                            if (valMes > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Month >= valMes).ToList();

                            if (valAno > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Year >= valAno).ToList();
                            break;

                        case "<=":
                            if (valDia > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Day <= valDia).ToList();

                            if (valMes > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Month <= valMes).ToList();

                            if (valAno > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Year <= valAno).ToList();
                            break;

                        case "<>":
                            if (valDia > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Day != valDia).ToList();

                            if (valMes > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Month != valMes).ToList();

                            if (valAno > 0)
                                query = query.AsEnumerable().Where(i => i.DataInicio.Year != valAno).ToList();
                            break;
                    }
                }
            }

            return query;
        }

        public bool registraProcesso(string empresa, string cnpj, string estadoEmpresa, string tipo, string situacao, string estado, string dataInicio, string valor, out string mensagem)
        {
            var processo = new Processos();

            bool result = false;
            string msg = String.Empty;

            decimal valProcesso = 0;
            DateTime valDataInicio;

            if (!String.IsNullOrEmpty(valor))
                valor = valor.Replace("R$", "").Replace(".", "");

            if (decimal.TryParse(valor, out valProcesso) && DateTime.TryParse(dataInicio, out valDataInicio))
            {
                processo.Empresa = empresa;
                processo.Cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                processo.EstadoEmpresa = estadoEmpresa;
                processo.Situacao = situacao;
                processo.Tipo = tipo;
                processo.Estado = estado;
                processo.Valor = valProcesso;
                processo.DataInicio = valDataInicio;

                if (new GerenciaDados(arquivo).adicionarXml(processo))
                {
                    msg = "Processo registrado com sucesso!";
                    result = true;
                }
                else
                    msg = "Falha ao registrar processo!";
            }else
                msg = "Falha ao registrar processo!";

            mensagem = msg;
            return result;
        }

        public bool alterarProcesso(string codigo, string empresa, string cnpj, string estadoEmpresa,  string tipo, string situacao, string estado, string dataInicio, string valor, out string mensagem)
        {
            var processo = new Processos();

            bool result = false;
            string msg = String.Empty;

            int id = 0;
            decimal valProcesso = 0;
            DateTime valDataInicio;

            if (!String.IsNullOrEmpty(valor))
                valor = valor.Replace("R$", "").Replace(".","");

            if (int.TryParse(codigo, out id) && decimal.TryParse(valor, out valProcesso) && DateTime.TryParse(dataInicio, out valDataInicio))
            {
                processo.Codigo = id;
                processo.Empresa = empresa;
                processo.Cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                processo.EstadoEmpresa = estadoEmpresa;
                processo.Situacao = situacao;
                processo.Tipo = tipo;
                processo.Estado = estado;
                processo.Valor = valProcesso;
                processo.DataInicio = valDataInicio;

                if (new GerenciaDados(arquivo).editarRegistroXml(processo))
                {
                    msg = "Processo alterado com sucesso!";
                    result = true;
                }
                else
                    msg = "Falha ao alterar processo!";
            }else
                msg = "Falha ao alterar processo!";

            mensagem = msg;
            return result;
        }

        public bool excluirProcesso(string id, out string mensagem)
        {
            bool result = false;
            string msg = String.Empty;

            int valId = 0;

            if (int.TryParse(id, out valId))
            {
                if (new GerenciaDados(arquivo).excluirRegistroXml(valId))
                {
                    msg = "Processo excluido com sucesso!";
                    result = true;
                }
                else
                    msg = "Falha ao excluir o processo!";
            }
            else
                msg = "Falha ao excluir o processo!";

            mensagem = msg;
            return result;
        }
    }
}
