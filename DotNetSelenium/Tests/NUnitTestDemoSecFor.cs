using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DotNetSelenium.Driver;
using DotNetSelenium.Pages__Sectiunea_3_;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium; => am refactorizat
//using OpenQA.Selenium.Chrome; => am refactorizat 
namespace DotNetSelenium.Tests;

[TestFixture("admin", "password", DriverType.Edge)]
public class NUnitTestDemoSecFor
{
    private IWebDriver _driver;
    private readonly string _userName;
    private readonly string _password;
    private readonly DriverType _driverType;
    private ExtentReports _extentReports;
    private ExtentTest _extentTest;
    private ExtentTest _extentNode;

    public NUnitTestDemoSecFor(string userName, string password, DriverType driverType)
    {
        _userName = userName;
        _password = password;
        _driverType = driverType;
    }

    [SetUp]
    public void Setup()
    {
        SetupExtentReports();      
        //_driver = new FirefoxDriver();
        _driver = GetDriverType(_driverType);
        _extentNode = _extentTest.CreateNode("Setup and TearDown Info").Pass("Browser launched");
        _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        _driver.Manage().Window.Maximize();
        //SetupExtentReports(); //apelez metoda in care am generat un raport de testare mai jos
    }

    private IWebDriver GetDriverType(DriverType driverType)
    {
        return driverType switch
        {
            DriverType.Chrome => new ChromeDriver(),
            DriverType.Firefox => new FirefoxDriver(),
            DriverType.Edge => new EdgeDriver(),
            _ => _driver
        }; 
        
        
                //o alta varianta de if, dar sunt multe linii de cod
        //switch (driverType)
        //{
        //    case DriverType.Chrome:
        //        _driver = new ChromeDriver();
        //        break;
        //    case DriverType.Firefox:
        //        _driver = new FirefoxDriver();
        //        break;
        //    case DriverType.Edge:
        //        _driver = new EdgeDriver();
        //        break;
        //}
        //return _driver; 

                //pot sa fac si cu if , dar tot sunt multe linii de cod
        //if (driverType == DriverType.Chrome)
        //{
        //    _driver = new ChromeDriver();
        //}
        //else if (driverType == DriverType.Firefox)
        //{
        //    _driver = new FirefoxDriver();
        //}
        //else if (driverType == DriverType.Edge)
        //{
        //    _driver = new EdgeDriver();
        //}
       // return _driver;
    }

    //Cum sa generezi un RAPORT  de Testare in Selenium (Sectiunea 11, curs 37)
    private void SetupExtentReports()
    {
        _extentReports = new ExtentReports();
        var spark = new ExtentSparkReporter("TestReports.html");
        _extentReports.AttachReporter(spark);
        _extentReports.AddSystemInfo("OS", "Windows 11");
        _extentReports.AddSystemInfo("Browser", _driverType.ToString());
        //pot sa folosesc acest extentTest pentru a face mai departe operatiuni de testare 
        _extentTest = _extentReports.CreateTest("Login Test with POM")
            .Log(Status.Info, "Extent report initialized");
        //Flush method va fi generata in [TearDown] la final dupa ce testele sunt terminate
        //, asa ca am scos-o de aici
    }


    [Test]
    [Category("smoke")]
    public void TestWithPOM()
    {
        //POM initialization
        LoginPage loginPage = new LoginPage(_driver);

        loginPage.ClickLogin();
        _extentTest.Log(Status.Pass, "Click Login");

        loginPage.Login(_userName, _password);
        _extentTest.Log(Status.Pass, "UserName an Password entered with login hapened");
        //wait
        //WebDriverWait driverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        ////driverWait.Until(drv => drv.FindElement(By.LinkText("Employee Details")));
        //IWebElement employeeDetailsLink = driverWait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Employee Details")));
        //driverWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        //Assert
        var getLoggedIn = loginPage.IsLoggedIn();
        Assert.IsTrue(getLoggedIn.employeeDetails & getLoggedIn.manageUser);
        _extentTest.Log(Status.Pass, ("Assertions successful"));
    }

    [Test]
    [TestCase("chrome", "30")]
    public void BrowserVersion(string browser, string version)
    {
        Console.WriteLine($"The browser is {browser} with version {version}"); //am folosit interpolarea pt operatiune
    }

    [TearDown]
    public void TearDown()
    {      
        _driver.Dispose(); //metoda Dispose() => imi va inchide aplicatia-browserul
        _extentNode.Pass("Browser quit");
        _extentReports.Flush();
    }
}
