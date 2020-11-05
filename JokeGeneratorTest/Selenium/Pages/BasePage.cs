using JokeGeneratorTests.Selenium.Framework;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace JokeGeneratorTests.Selenium.Pages
{
    public class BasePage
    {
        protected const string URL = "https://www.names.privserv.com/";

        public void Dispose()
        {
            Driver.Dispose();
        }

        public virtual bool IsPageDisplayed()
        {
            throw new NotImplementedException();
        }
    }
}
