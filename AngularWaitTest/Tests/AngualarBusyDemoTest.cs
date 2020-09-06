using System;
using System.Threading;
using NUnit.Framework;
using AngularWaitTest.Pages;
using OpenQA.Selenium;

namespace AngularWaitTest.Tests
{
    public class AngularBusyDemoTest : BaseTest
    {
        private AngularBusyDemoPage angularBusyDemoPage;
        private int delay;
        private int duration;
        private long TimeElapsed { get; set; }
        private string BusyText { get; set; }

        [OneTimeSetUp]
        public void SetUpPageObjects()
        {
            angularBusyDemoPage = new AngularBusyDemoPage(driver);
        }

        [Test, Order(1)]
        public void T01_CheckDelayFieldPresence() => Assert.IsTrue(angularBusyDemoPage.IsDelayFieldPresent());

        [Test, Order(2)]
        public void T02_CheckDurationFieldPresence() => Assert.IsTrue(angularBusyDemoPage.IsDurationFieldPresent());

        [Test, Order(3)]
        public void T03_CheckMessageFieldPresence() => Assert.IsTrue(angularBusyDemoPage.IsMessageFieldPresent());

        [Test, Order(4)]
        public void T04_CheckDemoButtonPresence() => Assert.IsTrue(angularBusyDemoPage.IsDemoButtonPresent());

        [Test, Order(5)]
        public void T05_CheckTableHeaders()
        {
            string[] expected = { "#", "First Name", "Last Name", "Username" };
            Assert.AreEqual(expected, angularBusyDemoPage.GetTableHeaders());
        }

        [Test, Order(6)]
        public void T06_SetDelay()
        {
            delay = 2;
            Assert.AreEqual(
                Int32.Parse(angularBusyDemoPage.SetDelay(delay * 1000)),
                delay * 1000
            );
        }
        
        [Test, Order(7)]
        public void T07_SetDuration()
        {
            duration = 10;
            Assert.AreEqual(
                Int32.Parse(angularBusyDemoPage.SetDuration(duration * 1000)),
                duration * 1000
                );
        }

        [Test, Order(8)]
        public void T08_SetMessage()
        {
            BusyText = "Test text";
            Assert.AreEqual(angularBusyDemoPage.SetMessage(BusyText), BusyText);
        }

        [Test, Order(9)]
        public void T09_UnsetBackDrop()
        {
            Assert.IsFalse(angularBusyDemoPage.SetUnSetBackDrop());
        }

        [Test, Order(10)]
        public void T10_SetBackDrop()
        {
            Assert.IsTrue(angularBusyDemoPage.SetUnSetBackDrop());
        }

        [Test, Order(11)]
        public void T11_SelectTemplate()
        {
            angularBusyDemoPage.SelectTemplate(true);
        }

        [Test, Order(12)]
        public void T12_ChecksIfStandardLoaderAppearsAfterDelay()
        {
            angularBusyDemoPage.ClickDemo();
            // get time after clicking
            long timeAtClicking = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            CalculateTimeElapsed(false);
            Assert.AreEqual(
                TimeElapsed - timeAtClicking,
                delay,
                1,
                $"Loader apperared after {delay * 1000} milli seconds"
                );
        }

        [Test, Order(13)]
        public void T13_CheckStandardLoaderText()
        {
            Assert.AreEqual(angularBusyDemoPage.CheckLoaderText(), BusyText);
        }

        [Test, Order(14)]
        public void T14_CheckStandardLoaderTextPresence()
        {
            Assert.IsTrue(
                angularBusyDemoPage.CheckForLoaderTextPresence(),
                "Loader text is not present"
                );
        }

        [Test, Order(15)]
        public void T15_CheckLoaderDuration()
        {
            angularBusyDemoPage.WaitForLoaderAbsence();
            long currentTime = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            long durationDisplayed = currentTime - TimeElapsed;
            Assert.AreEqual(
                durationDisplayed,
                duration,
                1,
                $"loader is displayed for {durationDisplayed * 1000} ms , while it should be displayed for {duration * 1000} ms"
               );          
        }

        [Test, Order(16)]
        public void T16_SelectCustomTemplate()
        {
            angularBusyDemoPage.SelectTemplate(false);
        }

        [Test, Order(17)]
        public void T17_ClicksOnDemo()
        {
            angularBusyDemoPage.ClickDemo();
        }

        [Test, Order(18)]
        public void T18_ChecksIfCustomeLoaderAppearsAfterDelay()
        {
            long timeAtClicking = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            CalculateTimeElapsed(true);
            Assert.AreEqual(
                TimeElapsed - timeAtClicking, delay,
                1,
                $"Loader apperared after {delay * 1000} milli seconds"
                );
        }

        [Test, Order(19)]
        public void T19_CheckCustomLoaderTextPresence()
        {
            Assert.IsTrue(
                angularBusyDemoPage.CheckForLoaderTextPresence(true),
                "Loader text is not present"
                );
        }

        [Test, Order(20)]
        public void T20_ChecksCustomLoaderDuration()
        {
            angularBusyDemoPage.WaitForLoaderAbsence();
            long currentTime = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            long durationDisplayed = currentTime - TimeElapsed;
            Assert.AreEqual(
                durationDisplayed,
                duration,
                $"loader is displayed for {durationDisplayed * 1000} ms , while it should be displayed for {duration * 1000} ms"
               );
        }

        public void CalculateTimeElapsed(bool isCustomeLoader = false)
        {
            try
            { 
                angularBusyDemoPage.WaitForLoader(isCustomeLoader);
            }
            catch (WebDriverTimeoutException e)
            {
                Assert.Fail("Loader not displayed");
                Console.WriteLine(e);
            }

            // get time elapsed
            TimeElapsed = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

    }
}
