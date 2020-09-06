using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AngularWaitTest.Tests
{

    public class BaseTest
    {
        protected IWebDriver driver;


        [OneTimeSetUp]
        public void SetUpBrowser()
        {

            driver = new ChromeDriver
            {
                Url = "http://cgross.github.io/angular-busy/demo/"
            };

        }


        [OneTimeTearDown]
        public void QuitDriver()
        {
            driver.Quit();
        }

    }
}
