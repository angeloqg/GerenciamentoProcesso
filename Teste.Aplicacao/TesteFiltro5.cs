using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace Teste.Aplicacao
{
    [TestClass]
    public class TesteFiltro5
    {
        [TestMethod]
        public void TesteFiltro(IWebDriver driver)
        {
            driver.FindElement(By.Id("campo1")).SendKeys("A");
            driver.FindElement(By.Id("campo3")).FindElement(By.CssSelector("option[value='RJ']")).Click();
            driver.FindElement(By.Id("filtrar")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.Id("listar")).Click();
            driver.FindElement(By.Id("campo1")).SendKeys("B");
            driver.FindElement(By.Id("campo3")).FindElement(By.CssSelector("option[value='SP']")).Click();
            driver.FindElement(By.Id("filtrar")).Click();
            Thread.Sleep(10000);
            driver.FindElement(By.Id("listar")).Click();
        }
    }
}
