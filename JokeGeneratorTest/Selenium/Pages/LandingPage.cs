using System;
using System.Collections.Generic;
using System.Text;
using JokeGeneratorTests.Selenium.Framework;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JokeGeneratorTests.Selenium.Pages
{
    public class LandingPage : BasePage
    {
        public LandingPage(string browserName)
        {
            Driver.initialize(browserName);
            Driver.WebDriver.Navigate().GoToUrl(URL);
        }

        private const string pressSpacebar_locator = "//*[@id = 'name'][text() = 'Press Spacebar']";
        private const string region_locator = "//*[@title = 'Select Region']";
        private const string region_country_base_locator = "//*[text() = '$#']";
        private const string photo_locator = "//a[contains(@href, 'https://names.privserv.com/api/photos')]";
        private const string name_locator = "//*[@id = 'name']";

        public override bool IsPageDisplayed()
        {
            return Driver.WebDriver.FindElement(By.XPath(pressSpacebar_locator)).Displayed;
        }

        public void clickRegion()
        {
            Driver.WebDriver.FindElement(By.XPath(region_locator)).Click();
        }

        public bool PerformRegionSearch(string region_input)
        {
            Driver.WebDriver.FindElement(By.Id("rsearch")).Clear();
            Driver.WebDriver.FindElement(By.Id("rsearch")).SendKeys(region_input);
            Driver.ImplicitWaitMS(1000);
            string region_country_locator = region_country_base_locator.Replace("$#", region_input);
            return Driver.WebDriver.FindElement(By.XPath(region_country_locator)).Displayed;
        }

        public bool PerformNameSearch()
        {
            Driver.WebDriver.SwitchTo().ActiveElement().SendKeys(Keys.Space);
            Driver.ImplicitWaitMS(1000);
            return Driver.WebDriver.FindElement(By.XPath(photo_locator)).Displayed;
        }

        public string ReadGeneratedName()
        {
            return Driver.WebDriver.FindElement(By.XPath(name_locator)).Text;
        }
    }
}
