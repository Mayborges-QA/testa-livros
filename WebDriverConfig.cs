using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace TesteBlueSoft
{
    public class WebDriverConfig

    {
        public IWebDriver driver;
        public WebDriverWait waiter;

        public WebDriverConfig()
        {
            var chromeOptions = new ChromeOptions();
            driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, chromeOptions);
            driver.Manage().Window.Maximize();
            waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(200));
        }
    }
}
