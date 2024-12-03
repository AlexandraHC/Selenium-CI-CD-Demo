using DotNetSelenium.Driver;
using DotNetSelenium.Pages__Sectiunea_3_;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace DotNetSelenium.Tests;

[TestFixture("username", "password", DriverType.Edge)]
public class SeleniumGridTests
{
    private IWebDriver _driver;
    private readonly string _userName;
    private readonly string _password;
    private readonly DriverType _driverType;

    public SeleniumGridTests(string userName, string password, DriverType driverType)
    {
        _userName = userName;
        _password = password;
        _driverType = driverType;
    }

    [SetUp]
    public void Setup()
    {
        //_driver = new FirefoxDriver();
        // _driver = GetDriverType(_driverType);

        _driver = new RemoteWebDriver(new Uri("http://localhost:4444"), new FirefoxOptions());

        _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        _driver.Manage().Window.Maximize();
    }


    //[Test]
    //[Category("smoke")]
    //public void TestWithPOM()
    //{
    //    //POM initialization
    //    LoginPage loginPage = new LoginPage(_driver);

    //    loginPage.ClickLogin();

    //    loginPage.Login(_userName, _password);
    //} 

    [Test]
    [TestCase("chrome", "30")]
    public void BrowserVersion(string browser, string version)
    {
        Console.WriteLine($"The browser is {browser} with version {version}"); //am folosit interpolarea pt operatiune
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Dispose();
    }
}
