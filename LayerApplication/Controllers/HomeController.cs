using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LayerBusiness;
using System.Globalization;
using LayerApplication.Models;

namespace LayerApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        #region Métodos de Alteração
        /// <summary>
        /// Inclui novo processo a partir da interface
        /// </summary>
        /// <param name="empresa"></param>
        /// <param name="cnpj"></param>
        /// <param name="tipo"></param>
        /// <param name="situacao"></param>
        /// <param name="estado"></param>
        /// <param name="dataInicio"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public JsonResult incluirNovoProcesso(string empresa, string cnpj, string estadoEmpresa, string tipo, string situacao, string estado, string dataInicio, string valor)
        {
            var resultado = new ResultadoModel();
            string arquivo = Server.MapPath("~/BaseDados/Processos.xml");
            string mensagem = String.Empty;

            var inclusao = new GestaoInformacoes(arquivo).registraProcesso(empresa, cnpj, estadoEmpresa, tipo, situacao, estado, dataInicio, valor, out mensagem);
 
             resultado.Mensagem = mensagem;

             return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Altera dados do processo a partir da interface
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="empresa"></param>
        /// <param name="cnpj"></param>
        /// <param name="tipo"></param>
        /// <param name="situacao"></param>
        /// <param name="estado"></param>
        /// <param name="dataInicio"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult alterarProcesso(string codigo, string empresa, string estadoEmpresa, string cnpj, string tipo, string situacao, string estado, string dataInicio, string valor)
        {
            var resultado = new ResultadoModel();
            string mensagem = String.Empty;
            string arquivo = Server.MapPath("~/BaseDados/Processos.xml");

            var alteracao = new GestaoInformacoes(arquivo).alterarProcesso(codigo, empresa, cnpj, estadoEmpresa, tipo, situacao, estado, dataInicio, valor, out mensagem);

            resultado.Mensagem = mensagem;

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Excluir registro do processo a partir da interface
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult excluirProcesso(string codigo)
        {
            var resultado = new ResultadoModel();
            string mensagem = String.Empty;
            string arquivo = Server.MapPath("~/BaseDados/Processos.xml");

            var exclusao = new GestaoInformacoes(arquivo).excluirProcesso(codigo, out mensagem);

            resultado.Mensagem = mensagem;

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Métodos de Retorno

        /// <summary>
        /// Carrega dados de processo para a interface
        /// </summary>
        /// <returns></returns>
        public JsonResult carregaProcessos()
        {
            string cnpj = String.Empty;
            var listaDados = new List<DadosModel>();
            var informacoes = new MensagemModel();

            string arquivo = Server.MapPath("~/BaseDados/Processos.xml");
            var listaProcessos = new GestaoInformacoes(arquivo).ListaProcessos(String.Empty,
                                                                        String.Empty,
                                                                        String.Empty,
                                                                        String.Empty,
                                                                        String.Empty,
                                                                        String.Empty,
                                                                        String.Empty,
                                                                        String.Empty,
                                                                        String.Empty,
                                                                        String.Empty,
                                                                        String.Empty);
            int numProcessos = 0;
            Decimal totalProcessos = 0;

            foreach(var valor in listaProcessos)
            {
                var processo = new DadosModel();
                
                processo.Codigo = valor.Codigo.ToString();
                processo.Empresa = valor.Empresa;

                cnpj = valor.Cnpj.Substring(0, 2) + ".";
                cnpj += valor.Cnpj.Substring(2, 3) + ".";
                cnpj += valor.Cnpj.Substring(5, 3) + "/";
                cnpj += valor.Cnpj.Substring(8, 4) + "-";
                cnpj += valor.Cnpj.Substring(12, 2);

                processo.Cnpj = cnpj;
                processo.EstadoEmpresa = valor.EstadoEmpresa;
                processo.Tipo = valor.Tipo;
                processo.Situacao = valor.Situacao;
                processo.Estado = valor.Estado;
                processo.Valor = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valor.Valor);
                processo.DataInicio = valor.DataInicio.ToString("dd/MM/yyyy");
                processo.Processo = valor.Processo;

                totalProcessos += Convert.ToDecimal(valor.Valor);
                listaDados.Add(processo);
                numProcessos++;
            }

            informacoes.Dados = listaDados;
            informacoes.NumeroProcessos = numProcessos;
            informacoes.ValorTotal = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", totalProcessos);
            informacoes.ValorMedio = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", totalProcessos/numProcessos);

            numProcessos = 0;

            return Json(informacoes, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Carrega dados de processo aplicando filtros
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipo"></param>
        /// <param name="situacao"></param>
        /// <param name="empresa"></param>
        /// <param name="oprBusca"></param>
        /// <param name="valorProcesso"></param>
        /// <param name="optBuscaData"></param>
        /// <param name="dia"></param>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult carregaProcessosFiltro(string id, string tipo, string situacao, string empresa, string oprBusca, string valorProcesso, string optBuscaData, string dia, string mes, string ano, string estado)
        {
            string cnpj = String.Empty;
            var listaDados = new List<DadosModel>();
            var informacoes = new MensagemModel();

            string arquivo = Server.MapPath("~/BaseDados/Processos.xml");
            var listaProcessos = new GestaoInformacoes(arquivo).ListaProcessos(id,
                                                                        tipo,
                                                                        situacao,
                                                                        empresa,
                                                                        oprBusca,
                                                                        valorProcesso,
                                                                        optBuscaData,
                                                                        dia,
                                                                        mes,
                                                                        ano,
                                                                        estado);

            int numProcessos = 0;
            Decimal totalProcessos = 0;

            foreach (var valor in listaProcessos)
            {
                DadosModel processo = new DadosModel();
                processo.Codigo = valor.Codigo.ToString();
                processo.Empresa = valor.Empresa;

                cnpj = valor.Cnpj.Substring(0, 2) + ".";
                cnpj += valor.Cnpj.Substring(2, 3) + ".";
                cnpj += valor.Cnpj.Substring(5, 3) + "/";
                cnpj += valor.Cnpj.Substring(8, 4) + "-";
                cnpj += valor.Cnpj.Substring(12, 2);

                processo.Cnpj = cnpj;
                processo.EstadoEmpresa = valor.EstadoEmpresa;
                processo.Tipo = valor.Tipo;
                processo.Situacao = valor.Situacao;
                processo.Estado = valor.Estado;
                processo.Valor = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valor.Valor);
                processo.DataInicio = valor.DataInicio.ToString("dd/MM/yyyy");
                processo.Processo = valor.Processo;

                totalProcessos += Convert.ToDecimal(valor.Valor);
                listaDados.Add(processo);
                numProcessos++;
            }

            informacoes.Dados = listaDados;
            informacoes.NumeroProcessos = numProcessos;
            informacoes.ValorTotal = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", totalProcessos);
            informacoes.ValorMedio = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", totalProcessos / numProcessos);

            numProcessos = 0;

            return Json(informacoes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RetornaDadosProcesso(string id)
        {
            string cnpj = String.Empty;
            DadosModel processo = new DadosModel();
            string arquivo = Server.MapPath("~/BaseDados/Processos.xml");
            try
            {
                var dados = new GestaoInformacoes(arquivo).RetornaDadosProcesso(id);

                cnpj = dados.Cnpj.Substring(0, 2) + ".";
                cnpj += dados.Cnpj.Substring(2, 3) + ".";
                cnpj += dados.Cnpj.Substring(5, 3) + "/";
                cnpj += dados.Cnpj.Substring(8, 4) + "-";
                cnpj += dados.Cnpj.Substring(12, 2);


                processo.Codigo = dados.Codigo.ToString();
                processo.Empresa = dados.Empresa;
                processo.EstadoEmpresa = dados.EstadoEmpresa;
                processo.Cnpj = cnpj;
                processo.Tipo = dados.Tipo;
                processo.Situacao = dados.Situacao;
                processo.Estado = dados.Estado;
                processo.DataInicio = dados.DataInicio.ToString("dd/MM/yyyy");
                processo.Valor = dados.Valor.ToString();
                processo.Processo = dados.Processo;
            }
            catch
            {
                // -- Sem ação
            }

            return Json(processo, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}