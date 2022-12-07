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
    public class LogInPage : HtmlPage
    {
        [FindsBy(How = How.Id, Using = "Input_Email")]
        private HtmlInput email;

        [FindsBy(How = How.Id, Using = "Input_Password")]
        private HtmlInput password;

        [FindsBy(How = How.Id, Using = "login-submit")]
        private HtmlInput logInButton;

        public LogInPage(ISearchContext webDriverOrWrapper) : base(webDriverOrWrapper)
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
        public MainPage LogInAndGoToMainPage()
        {
            logInButton.Click();
            return PageObjectFactory.Create<MainPage>(this);
        }
    }
}
