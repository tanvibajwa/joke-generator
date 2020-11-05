using System;
using System.Collections.Generic;
using System.Text;

using JokeGeneratorTests.Selenium.Framework;
using JokeGeneratorTests.Selenium.Pages;

using NUnit.Framework;

namespace JokeGeneratorTests.Selenium.Test.privserv
{
    [TestFixture("chrome")]
    [TestFixture("firefox")]
    class LandingPageTests
    {
        private string browserName { get; set; }

        private LandingPage landingPage { get; set; }


        public LandingPageTests(string browserName)
        {
            this.browserName = browserName;
        }

        [SetUp]
        public void setup()
        {
            landingPage = new LandingPage(browserName);
        }

        [TearDown]
        public void teardown()
        {
            landingPage.Dispose();
        }

        [Test]
        public void verify_that_page_is_displayed()
        {
            Assert.True(landingPage.IsPageDisplayed());
        }

        [Test]
        public void verify_new_names_are_generated()
        {
            for (int i = 0; i < 5; i++)
            {
                Assert.True(landingPage.PerformNameSearch());
                var name = landingPage.ReadGeneratedName();
                Assert.False(string.IsNullOrEmpty(name));
                Assert.False(string.IsNullOrWhiteSpace(name));
            }

        }

        [Test]
        public void verify_region_can_be_filtered()
        {
            landingPage.clickRegion();
            Assert.True(landingPage.PerformRegionSearch("Canada"));
            Assert.True(landingPage.PerformRegionSearch("India"));
            Assert.True(landingPage.PerformRegionSearch("China"));
        }
    }
}
