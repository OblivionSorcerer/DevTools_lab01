using HtmlElements.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Northwind.Web.Tests.SeleniumTests.Pages
{
    public class MainPage : HtmlPage
    {
        [FindsBy(How = How.CssSelector, Using = "a[href*='Categories'].nav-link")]
        private HtmlLink categoriesLink;

        [FindsBy(How = How.CssSelector, Using = "a[href*='Identity/Account/Register'].nav-link")]
        private HtmlLink registerLink;

        [FindsBy(How = How.CssSelector, Using = "a[href*='Identity/Account/Login'].nav-link")]
        private HtmlLink logInLink;

        [FindsBy(How = How.CssSelector, Using = "a[href*='Identity/Account/Manage'].nav-link")]
        private HtmlLink profileLink;

        [FindsBy(How = How.CssSelector, Using = "body > header > nav > div > div > ul > li:nth-child(2) > form > button")]
        private HtmlLink exitLink;

        public MainPage(ISearchContext webDriverOrWrapper) : base(webDriverOrWrapper)
        {
        }
        public string ExitText
        {
            get{ return exitLink.Text; }
        }
        public string RegisterText
        {
            get { return registerLink.Text; }
        }
        public string EnterText
        {
            get { return logInLink.Text; }
        }
        public MainPage LogOut()
        {
            exitLink.Click();
            return PageObjectFactory.Create<MainPage>(this);
        }
        public RegisterPage GoToRegisterPage()
        {
            registerLink.Click();
            return PageObjectFactory.Create<RegisterPage>(this);
        }
        public LogInPage GoToLogInPage()
        {
            logInLink.Click();
            return PageObjectFactory.Create<LogInPage>(this);
        }
        public ProfilePage GoToProfilePage()
        {
            profileLink.Click();
            return PageObjectFactory.Create<ProfilePage>(this);
        }

        public CategoryListPage GoToCategoriesListPage()
        {
            categoriesLink.Click();
            return PageObjectFactory.Create<CategoryListPage>(this);
        }
    }
}
