using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Xunit;
using TesteBlueSoft.src;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Diagnostics;

namespace TesteBlueSoft.src
{
    public class Teste : IClassFixture<WebDriverConfig>
    {
        readonly WebDriverConfig web;

        public Teste(WebDriverConfig webDriverConfig)
        {
            this.web = webDriverConfig;
        }

        [Fact]

        public void LocalizaLivros()
        {
            //Acessar o site submarino
            web.driver.Navigate().GoToUrl("https://www.submarino.com.br");

            //pesquisar por livros
            var searchSubmarino = web.driver.FindElement(By.CssSelector("#h_search-input"));
            searchSubmarino.SendKeys("Livros");
            web.waiter.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#h_search > div > div.src-suggestion > ul > li.as-lst-it.sz.sz-1"))).Click();

            //guardar Autor
            var autorSubmarino = web.waiter.Until(ExpectedConditions.ElementExists(By.CssSelector("#info-section > div:nth-child(2) > section > div.ColUI-gjy0oc-0.cxaHDZ.ViewUI-sc-1ijittn-6.iXIDWU > section > table > tbody > tr:nth-child(8) > td:nth-child(2) > span")));
            string autorSubmarinoText = autorSubmarino.Text;

            //guardar isbn submarino
            var isbnSubmarino = web.waiter.Until(ExpectedConditions.ElementExists(By.CssSelector("#info-section > div:nth-child(2) > section > div.ColUI-gjy0oc-0.cxaHDZ.ViewUI-sc-1ijittn-6.iXIDWU > section > table > tbody > tr:nth-child(10) > td:nth-child(2) > span")));
            string isbnSubmarinoText = isbnSubmarino.Text;

            //acessar americanas
            web.driver.Navigate().GoToUrl("https://www.americanas.com.br");

            //buscar por ISBN
            var searchAmericanas = web.waiter.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#h_search-input")));
            searchAmericanas.SendKeys(isbnSubmarinoText);
            searchAmericanas.Submit();
            web.driver.FindElement(By.CssSelector("#content-middle > div:nth-child(6) > div > div > div > div.row.product-grid.no-gutters.main-grid > div > div > div.RippleContainer-sc-1rpenp9-0.dMCfqq > a > section > div.product-card-photo.Photo-bwhjk3-4.feDKYY.ViewUI-sc-1ijittn-6.iXIDWU > div > div > picture > img")).Click();

            //Verificar autor na Americanas
            var autorAmericanas = web.waiter.Until(ExpectedConditions.ElementExists(By.CssSelector("#info-section > div:nth-child(2) > section > div > div.ColUI-gjy0oc-0.cxaHDZ.ViewUI-sc-1ijittn-6.iXIDWU > table > tbody > tr:nth-child(8) > td:nth-child(2) > span")));
            string autorAmericanasText = autorAmericanas.Text;

            //Comparar autores SubmarixoXAmericanas
            
            try
            {
                Assert.Equal(autorSubmarinoText, autorAmericanasText);
            }
            catch (Exception)
            {
                Console.WriteLine("Os autores são divergentes");
                throw;
            }

            //Acessar a Amazon
            web.driver.Navigate().GoToUrl("https://www.livrariacultura.com.br");
            
            //buscar por ISBN
            var searchAmazon = web.driver.FindElement(By.Id("Ntt-responsive"));
            searchAmazon.SendKeys(isbnSubmarinoText);
            searchAmazon.Submit();
            
            //Encerrar navegador
            web.driver.Quit();
            

        }

    }
}
