using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace Teste.Aplicacao
{
    [TestClass]
    public class TesteFiltro4
    {
        [TestMethod]
        public void TesteFiltro(IWebDriver driver)
        {
            driver.FindElement(By.Id("campo7")).SendKeys("9");
            driver.FindElement(By.Id("filtrar")).Click();
            Thread.Sleep(10000);
            driver.FindElement(By.Id("listar")).Click();
        }
    }
}
