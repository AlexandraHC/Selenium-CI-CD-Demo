using DotNetSelenium.Extensions;

namespace DotNetSelenium.Pages__Sectiunea_3_;

public class LoginPage(IWebDriver _driver)    //am pus aici parametrul driver pentru a nu mai avea constructor primar si ramane default
{
    // ************ #Prior to C#12

    //private readonly IWebDriver _driver;
    //public LoginPage(IWebDriver driver)
    //{
    //    _driver = driver;
    //}

    // *********************************

    IWebElement LoginLink => _driver.FindElement(By.Id("loginLink"));  // CU semnul "=>" Lambda Expression am facut proprietatea

    IWebElement TxtUserName => _driver.FindElement(By.Name("UserName"));

    IWebElement TxtPassword => _driver.FindElement(By.Id("Password"));

    IWebElement BtnLogin => _driver.FindElement(By.CssSelector(".btn"));

    IWebElement LnkEmployeeDetails => _driver.FindElement(By.LinkText("Employee Details"));

    IWebElement LnkManageUser => _driver.FindElement(By.LinkText("Manage Users"));

    IWebElement LnkLogOff => _driver.FindElement(By.LinkText("Log Off"));

    public void ClickLogin()
    {
        LoginLink.Click();
        // SectionTwoSeleniumCustomMethods.Click(LoginLink);   // in loc de ->> LoginLink.Click();
    }

    public void Login(string username, string password)
    {
        TxtUserName.EnterText(username);
        TxtPassword.SendKeys(password);
        BtnLogin.SubmitElement();
        //SectionTwoSeleniumCustomMethods.EnterText(TxtUserName, username);//in loc de -> TxtUserName.SendKeys(username);
        //SectionTwoSeleniumCustomMethods.EnterText(TxtPassword, password);//in loc de -> TxtUserName.SendKeys(password);
        //SectionTwoSeleniumCustomMethods.Submit(BtnLogin);        // in loc de ->>BtnLogin.Submit();
    }

    public (bool employeeDetails, bool manageUser) IsLoggedIn()
    {
        return (LnkEmployeeDetails.Displayed, LnkManageUser.Displayed);
    }


}
