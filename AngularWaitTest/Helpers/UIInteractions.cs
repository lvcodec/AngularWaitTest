using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace AngularWaitTest.Helpers
{
    /// <summary>
    /// Helper class to do ui actions like click, wait for presence etc..,
    /// </summary>
    public class UIInteractions
    {
        private readonly WebDriverWait wait;
        private readonly IJavaScriptExecutor jsExecutor;

        /// <summary>
        /// sets Initializes WebdriverWait, IJavaScriptExecutor objects 
        /// </summary>
        /// <param name="driver">IWebdriver instance</param>
        public UIInteractions(IWebDriver driver)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            jsExecutor = (IJavaScriptExecutor)driver;
        }

        /// <summary>
        /// waits for the element to become clickable
        /// </summary>
        /// <param name="e">element to wait for</param>
        /// <returns>IWebElement returned from expected condtions</returns>
        public IWebElement WaitForClickability(IWebElement e)
        {
            return wait.Until(ExpectedConditions.ElementToBeClickable(e));
        }

        /// <summary>
        /// waits for the element to become present
        /// </summary>
        /// <param name="locator">locator to be present</param>
        /// <returns>IWebElement returned from expected condtions</returns>
        public IWebElement WaitForPresence(By locator)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        /// <summary>
        /// clicks on an element after expected conditions are met
        /// </summary>
        /// <param name="e">element to click on</param>
        public void ClickOnElement(IWebElement e)
        {
            WaitForClickability(e).Click();
        }

        /// <summary>
        /// clears and sets text in a text field
        /// </summary>
        /// <param name="e">text field to enter text</param>
        /// <param name="text">text to enter</param>
        public void SetText(IWebElement e, string text)
        {
            e = WaitForClickability(e);
            e.Clear();
            e.SendKeys(text.Length > 0 ? text : throw new Exception("String of length > 0 is expected"));
        }

        /// <summary>
        /// checks if a given field is present
        /// </summary>
        /// <param name="e"></param>
        /// <returns>true if its present, false otherwise</returns>
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

        /// <summary>
        /// checks if a given field is absent
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>true if absent</returns>
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

        /// <summary>
        /// clicks using js executor
        /// </summary>
        /// <param name="e">Element to click on</param>
        public void ClickUsingJs(IWebElement e)
        {
            jsExecutor.ExecuteScript("arguments[0].click();", e);
        }
    }
}
