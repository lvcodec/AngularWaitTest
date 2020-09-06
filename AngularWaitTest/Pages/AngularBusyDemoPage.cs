using OpenQA.Selenium;
using AngularWaitTest.Helpers;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;
using System.Collections.Generic;

namespace AngularWaitTest.Pages
{
    public class AngularBusyDemoPage
    {
        #pragma warning disable 0649

        [FindsBy(How = How.CssSelector, Using = "#delayInput")]
        public IWebElement Delay;

        [FindsBy(How = How.CssSelector, Using = "#durationInput")]
        private readonly IWebElement Duration;

        [FindsBy(How = How.Id, Using = "message")]
        private readonly IWebElement MessageBox;

        [FindsBy(How = How.CssSelector, Using = "option[value*='custom']")]
        private readonly IWebElement CustomLoaderOption;

        [FindsBy(How = How.CssSelector, Using = ".row button")]
        private readonly IWebElement Demo;

        [FindsBy(How = How.XPath, Using = "//div[@ng-show='$cgBusyIsActive()']")]
        private readonly IWebElement HiddenLoader;

        [FindsBy(How = How.CssSelector, Using = ".checkbox input")]
        private readonly IWebElement Backdrop;

        [FindsBy(How = How.CssSelector, Using = ".cg-busy-default-text")]
        private readonly IWebElement LoaderText;

        [FindsBy(How = How.CssSelector, Using = "div[style*='background'] > div")]
        private readonly IWebElement CustomLoaderText;

        [FindsBy(How = How.CssSelector, Using = "table th")]
        private readonly IList<IWebElement> TableHeaders;

        private By loader;

        private By customLoader;

        private readonly UIInteractions ui;

        /// <summary>
        /// constructor that would initialize driver and page objects
        /// </summary>
        /// <param name="driver"> of type IWebDriver</param>
        public AngularBusyDemoPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            ui = new UIInteractions(driver);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsDelayFieldPresent() => ui.FieldPresence(Delay);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsDurationFieldPresent() => ui.FieldPresence(Duration);

        public bool IsMessageFieldPresent() => ui.FieldPresence(MessageBox);

        public bool IsTemplateDropDownPresent() => ui.FieldPresence(CustomLoaderOption);

        public bool IsDemoButtonPresent() => ui.FieldPresence(Demo);
        
        /// <summary>
        /// sets delay text field with milli second delay
        /// </summary>
        /// <param name="ms">time in milli seconds</param>
        public string SetDelay(int ms)
        {
            ui.SetText(Delay, ms.ToString());
            return Delay.GetAttribute("value");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public string SetDuration(int ms)
        {
            ui.SetText(Duration, ms.ToString());
            return Duration.GetAttribute("value");

        }

        /// <summary>
        /// Enters a g
        /// </summary>
        /// <param name="message"></param>
        /// <returns>String entered</returns>
        public string SetMessage(string message = "test")
        {
            ui.SetText(MessageBox, message.Length > 0 ? message : "test");
            return MessageBox.GetAttribute("value");
        }

        public bool SetUnSetBackDrop()
        {
            ui.ClickUsingJs(Backdrop);
            try
            {
                return Backdrop.Selected;
            }
            catch(Exception)
            {
                return false;
            }

        }

        public void SelectTemplate(bool isStandard)
        {
            if (!isStandard)
            {
                ui.ClickOnElement(CustomLoaderOption);
            }
        }

        public void ClickDemo()
        {
            ui.ClickOnElement(Demo);
        }

        public bool WaitForLoader(bool isCustomLoader = false)
        {
            customLoader = By.CssSelector(".cg-busy-animation div[style*='background']");
            loader = By.CssSelector(".cg-busy-default-sign .cg-busy-default-spinner");

            if (isCustomLoader)
            {
                return ui.WaitForPresence(customLoader).Displayed;
            }
            else
            {
                return ui.WaitForPresence(loader).Displayed;
            }
        }

        public bool CheckForLoaderTextPresence(bool isCustom = false)
        {
            if(isCustom)
            {
                return CustomLoaderText.Displayed;
            }
            return LoaderText.Displayed;
        }

        public string CheckLoaderText()
        {
            return LoaderText.GetAttribute("innerText");
        }

        public void WaitForLoaderAbsence()
        {
            string expectedCss = "cg-busy cg-busy-animation ng-scope ng-hide";
            string actualCss;
            do
            {
                actualCss = HiddenLoader.GetAttribute("class");
            } while (actualCss != expectedCss);
           
        }

        public int GetTableColumns()
        {
            return TableHeaders.Count;
        }

        public string[] GetTableHeaders()
        {
            string[] headers = new string[TableHeaders.Count];
            int index = 0;
            foreach(IWebElement e in TableHeaders) {
                headers[index] = e.Text;
                index++;
            }
            return headers;
        }

    }
}
