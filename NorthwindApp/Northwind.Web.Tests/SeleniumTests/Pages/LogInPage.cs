using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlElements.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Northwind.Web.Tests.SeleniumTests.Pages
{
    public class LogIn : HtmlPage
    {
        [FindsBy(How = How.Id, Using = "Input.Email")]
        private HtmlInput email;

        [FindsBy(How = How.Id, Using = "Input.Password")]
        private HtmlInput password;

        [FindsBy(How = How.CssSelector, Using = "input[type='submit']")]
        private HtmlInput logInButton;

        public LogIn(ISearchContext webDriverOrWrapper) : base(webDriverOrWrapper)
        {
        }
        public string Email
        {
            get { return email.Value; }
            set { email.SendKeys(value); }
        }
        public string Password
        {
            get { return password.Value; }
            set { password.SendKeys(value); }
        }
        public LogIn LogInAndGoToMainPage()
        {
            logInButton.Click();
            return PageObjectFactory.Create<LogIn>(this);
        }
    }
}
