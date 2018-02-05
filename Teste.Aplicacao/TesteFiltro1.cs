using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace Teste.Aplicacao
{
    [TestClass]
    public class TesteFiltro1
    {
        [TestMethod]
        public void TesteFiltro(IWebDriver driver)
        {
            driver.FindElement(By.Id("campo2")).FindElement(By.CssSelector("option[value='Ativo']")).Click();
            driver.FindElement(By.Id("filtrar")).Click();
            Thread.Sleep(10000);
            driver.FindElement(By.Id("listar")).Click();
        }
    }
}
