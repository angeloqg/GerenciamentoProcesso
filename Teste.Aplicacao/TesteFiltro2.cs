using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace Teste.Aplicacao
{
    [TestClass]
    public class TesteFiltro2
    {
        [TestMethod]
        public void TesteFiltro(IWebDriver driver)
        {
            driver.FindElement(By.Id("campo1")).SendKeys("A");
            driver.FindElement(By.Id("campo3")).FindElement(By.CssSelector("option[value='RJ']")).Click();
            driver.FindElement(By.Id("filtrar")).Click();
            Thread.Sleep(10000);
            driver.FindElement(By.Id("listar")).Click();
        }
    }
}
