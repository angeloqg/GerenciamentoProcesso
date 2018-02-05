using LayerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace LayerData
{
    public class GerenciaDados
    {
        string arquivo = String.Empty;

        public GerenciaDados()
        {

        }

        public GerenciaDados(string arquivo)
        {
            this.arquivo = arquivo;
        }


        // -- Carrega os registros do arquivo XML
        public List<Processos> listarXml()
        {
            List<Processos> lista = new List<Processos>();
            try
            {
                if (File.Exists(arquivo))
                {
                    try
                    {
                        XDocument doc = XDocument.Load(arquivo);

                        var dados = from i in doc.Descendants("processo") select i;
                        int total = dados.Count();
                        foreach (var i in dados)
                        {
                            var valor = new Processos();
                            valor.Codigo = Convert.ToInt32(i.Element("codigo").Value);
                            valor.Empresa = i.Element("empresa").Value;
                            valor.Cnpj = i.Element("cnpj").Value;
                            valor.EstadoEmpresa = i.Element("estadoEmpresa").Value;
                            valor.Tipo = i.Element("tipo").Value;
                            valor.Situacao = i.Element("situacao").Value;
                            valor.Estado = i.Element("estado").Value;
                            valor.DataInicio = Convert.ToDateTime(i.Element("dataInicio").Value);
                            valor.Valor = Convert.ToDecimal(i.Element("valor").Value);
                            valor.Processo = i.Element("codigo").Value.PadLeft(5,'0') + i.Element("tipo").Value + i.Element("estado").Value;
                            lista.Add(valor);
                        }
                    }
                    catch
                    {
                        // -- Sem ação
                    }
                }
            }catch
            {
                // -- Sem ação
            }

            return lista;
        }

        // -- Adiciona elementos a um arquivo XML
        public bool adicionarXml(Processos processo)
        {
            bool result = false;

            try
            {
                try
                {
                    using (DataSet dsXml = new DataSet())
                    {
                        if (!File.Exists(arquivo))
                        {
                            // -- Cria um novo arquivo XML e adiciona um novo elemento
                            XmlTextWriter writer = new XmlTextWriter(arquivo, System.Text.Encoding.UTF8);

                            writer.WriteStartDocument(true);
                            writer.Formatting = Formatting.Indented;
                            writer.Indentation = 2;
                            writer.WriteStartElement("processos");

                            writer.WriteStartElement("processo");
                            writer.WriteStartElement("codigo");
                            writer.WriteString("1");
                            writer.WriteEndElement();

                            writer.WriteStartElement("empresa");
                            writer.WriteString(processo.Empresa);
                            writer.WriteEndElement();

                            writer.WriteStartElement("cnpj");
                            writer.WriteString(processo.Cnpj.Replace(".", "").Replace("/", "").Replace("-", ""));
                            writer.WriteEndElement();

                            writer.WriteStartElement("estadoEmpresa");
                            writer.WriteString(processo.EstadoEmpresa);
                            writer.WriteEndElement();

                            writer.WriteStartElement("tipo");
                            writer.WriteString(processo.Tipo);
                            writer.WriteEndElement();

                            writer.WriteStartElement("situacao");
                            writer.WriteString(processo.Situacao);
                            writer.WriteEndElement();

                            writer.WriteStartElement("estado");
                            writer.WriteString(processo.Estado);
                            writer.WriteEndElement();

                            writer.WriteStartElement("dataInicio");
                            writer.WriteString(processo.DataInicio.ToString("dd/MM/yyyy"));
                            writer.WriteEndElement();

                            writer.WriteStartElement("valor");
                            writer.WriteString(processo.Valor.ToString());
                            writer.WriteEndElement();

                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                            writer.Close();
                            dsXml.ReadXml(arquivo);

                            result = true;
                        }
                        else
                        {
                            dsXml.ReadXml(arquivo);
                            var verifica = listarXml().OrderByDescending(i => i.Codigo);

                            int codigo = (verifica.Count() >= 1) ? verifica.First().Codigo + 1 : 1;
                            
                            if(codigo > 1)
                            {
                                // -- Adiciona novo elemento a um XML existente
                                dsXml.Tables[0].Rows.Add(dsXml.Tables[0].NewRow());
                                dsXml.Tables[0].Rows[dsXml.Tables[0].Rows.Count - 1]["codigo"] = codigo.ToString();
                                dsXml.Tables[0].Rows[dsXml.Tables[0].Rows.Count - 1]["empresa"] = processo.Empresa.ToString();
                                dsXml.Tables[0].Rows[dsXml.Tables[0].Rows.Count - 1]["cnpj"] = processo.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                                dsXml.Tables[0].Rows[dsXml.Tables[0].Rows.Count - 1]["estadoEmpresa"] = processo.EstadoEmpresa;
                                dsXml.Tables[0].Rows[dsXml.Tables[0].Rows.Count - 1]["tipo"] = processo.Tipo;
                                dsXml.Tables[0].Rows[dsXml.Tables[0].Rows.Count - 1]["situacao"] = processo.Situacao;
                                dsXml.Tables[0].Rows[dsXml.Tables[0].Rows.Count - 1]["estado"] = processo.Estado;
                                dsXml.Tables[0].Rows[dsXml.Tables[0].Rows.Count - 1]["dataInicio"] = processo.DataInicio.ToString("dd/MM/yyyy");
                                dsXml.Tables[0].Rows[dsXml.Tables[0].Rows.Count - 1]["valor"] = processo.Valor.ToString();

                                dsXml.AcceptChanges();

                                //--  Escreve para o arquivo XML final usando o método Write
                                dsXml.WriteXml(arquivo, XmlWriteMode.IgnoreSchema);
                            }else
                            {
                                // -- Cria um novo arquivo XML e adiciona um novo elemento
                                XmlTextWriter writer = new XmlTextWriter(arquivo, System.Text.Encoding.UTF8);

                                writer.WriteStartDocument(true);
                                writer.Formatting = Formatting.Indented;
                                writer.Indentation = 2;
                                writer.WriteStartElement("processos");

                                writer.WriteStartElement("processo");
                                writer.WriteStartElement("codigo");
                                writer.WriteString("1");
                                writer.WriteEndElement();

                                writer.WriteStartElement("empresa");
                                writer.WriteString(processo.Empresa);
                                writer.WriteEndElement();

                                writer.WriteStartElement("cnpj");
                                writer.WriteString(processo.Cnpj.Replace(".", "").Replace("/", "").Replace("-", ""));
                                writer.WriteEndElement();

                                writer.WriteStartElement("estadoEmpresa");
                                writer.WriteString(processo.EstadoEmpresa);
                                writer.WriteEndElement();

                                writer.WriteStartElement("tipo");
                                writer.WriteString(processo.Tipo);
                                writer.WriteEndElement();

                                writer.WriteStartElement("situacao");
                                writer.WriteString(processo.Situacao);
                                writer.WriteEndElement();

                                writer.WriteStartElement("estado");
                                writer.WriteString(processo.Estado);
                                writer.WriteEndElement();

                                writer.WriteStartElement("dataInicio");
                                writer.WriteString(processo.DataInicio.ToString("dd/MM/yyyy"));
                                writer.WriteEndElement();

                                writer.WriteStartElement("valor");
                                writer.WriteString(processo.Valor.ToString());
                                writer.WriteEndElement();

                                writer.WriteEndElement();
                                writer.WriteEndDocument();
                                writer.Close();
                                dsXml.ReadXml(arquivo);
                            }
                      
                            result = true;
                        }                    
                    }
                }
                catch(Exception e)
                {
                    // -- Sem ação
                }
            }
            catch
            {
                // -- Sem ação
            }

            return result;
        }

        // -- Excluir elemento a um arquivo XML
        public bool excluirRegistroXml(int codigo)
        {
            bool result = false;

            try
            {
                if (File.Exists(arquivo))
                {

                    XElement xml = XElement.Load(arquivo);
                    var a = xml.Elements().ToList();
                    foreach(var i in a)
                    {
                        if(i.Element("codigo").Value == codigo.ToString())
                        {
                            i.Remove();                     
                        }
                    }

                    xml.Save(arquivo);
                    result = true;
                }
            }
            catch
            {
                // -- Sem ação
            }

            return result;
        }

        public bool editarRegistroXml(Processos processo)
        {
            bool result = false;

            try
            {
                if (File.Exists(arquivo))
                {

                    XElement xml = XElement.Load(arquivo);
                    var a = xml.Elements().ToList();
                    foreach (var i in a)
                    {
                        if (i.Element("codigo").Value == processo.Codigo.ToString())
                        {
                            i.Element("empresa").Value = processo.Empresa;
                            i.Element("cnpj").Value = processo.Empresa;
                            i.Element("cnpj").Value = processo.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                            i.Element("estadoEmpresa").Value = processo.EstadoEmpresa;
                            i.Element("tipo").Value = processo.Tipo;
                            i.Element("situacao").Value = processo.Situacao;
                            i.Element("estado").Value = processo.Estado;
                            i.Element("dataInicio").Value = processo.DataInicio.ToString("dd/MM/yyyy");
                            i.Element("valor").Value = processo.Valor.ToString();

                            i.Add();
                        }
                    }

                    xml.Save(arquivo);
                    result = true;
                }
            }
            catch (Exception error)
            {
                string t = error.Message;
            }
            return result;
        }
    }
}
