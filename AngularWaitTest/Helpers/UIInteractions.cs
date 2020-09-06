using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace AngularWaitTest.Helpers
{
    public class UIInteractions
    {
        private readonly WebDriverWait wait;
        private readonly IJavaScriptExecutor jsExecutor;

        public UIInteractions(IWebDriver driver)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            jsExecutor = (IJavaScriptExecutor)driver;
        }

        public IWebElement WaitForClickability(IWebElement e)
        {
            return wait.Until(ExpectedConditions.ElementToBeClickable(e));
        }

        public IWebElement WaitForPresence(By locator)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public void ClickOnElement(IWebElement e)
        {
            WaitForClickability(e).Click();
        }

        public void SetText(IWebElement e, string text)
        {
            e = WaitForClickability(e);
            e.Clear();
            e.SendKeys(text.Length > 0 ? text : throw new Exception("String of length > 0 is expected"));
        }

        public bool FieldPresence(IWebElement e)
        {
            try
            {
                return WaitForClickability(e).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool FieldAbsence(By locator)
        {
            try
            {
                return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
            }
            catch(Exception)
            {
                Console.WriteLine("FALSE");
                return false;
            }
        }

        public void ClickUsingJs(IWebElement e)
        {
            jsExecutor.ExecuteScript("arguments[0].click();", e);
        }
    }
}
