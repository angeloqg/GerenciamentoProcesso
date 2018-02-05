using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Teste.Aplicacao
{
    [TestClass]
    public class ExecutarTestes
    {
        [TestMethod]
        public void TestMethod1()
        {
            // -- Inicializar Teste
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:16720/");
            driver.Manage().Window.Maximize();

            // -- Teste 1 : Calcular a soma dos processos ativos. A aplicação deve retornar R$ 1.087.000,00.
            new TesteFiltro1().TesteFiltro(driver);

            // -- Teste 2 : Calcular a a média do valor dos processos no Rio de Janeiro para o Cliente "Empresa A". A aplicação deve retornar R$ 110.000,00.
            new TesteFiltro2().TesteFiltro(driver);

            // -- Teste 3 : Calcular o Número de processos com valor acima de R$ 100.000,00. A aplicação deve retornar 2.
            new TesteFiltro3().TesteFiltro(driver);

            // -- Teste 4 : Obter a lista de Processos de Setembro de 2007. A aplicação deve retornar uma lista com somente o Processo “00010TRABAM”.
            new TesteFiltro4().TesteFiltro(driver);

            // -- Teste 5 : Obter a lista de processos no mesmo estado do cliente, para cada um dos clientes. A aplicação deve retornar uma lista com os processos de número “00001CIVELRJ”,”00004CIVELRJ” para o Cliente "Empresa A" e “00008CIVELSP”,”00009CIVELSP” para o o Cliente "Empresa B".
            new TesteFiltro5().TesteFiltro(driver);

            // -- Teste 6 : Obter a lista de processos que contenham a sigla “TRAB”. A aplicação deve retornar uma lista com os processos “00003TRABMG” e “00010TRABAM”.
            new TesteFiltro6().TesteFiltro(driver);

            /* Atenção: Foi criado testes unitários para execução em massa.
             *          O propósto de cada teste é simular, utilizando Selenium, 
             *          a filtragem na aplicação, obtendo os casos de testes propostos.
             *          
             *          Para funcionar o teste, execute uma instância do projeto em separado primeiro.
             *          Após isso, execute o projeto Teste.Aplicacao, a classe ExecutarTestes.            
             */
        }
    }
}
