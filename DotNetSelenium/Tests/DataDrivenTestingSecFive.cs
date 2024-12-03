using DotNetSelenium.Pages__Sectiunea_3_;
using System.Text.Json;
using FluentAssertions;
using DotNetSelenium.Models;
using OpenQA.Selenium.Edge;

namespace DotNetSelenium.Tests.Tests;

[TestFixture("username", "password")]
public class DataDrivenTestingSecFive
{
    private IWebDriver _driver;
    private readonly string _userName;
    private readonly string _password;

    public DataDrivenTestingSecFive(string userName, string password)
    {
        _userName = userName;
        _password = password;
    }

    [SetUp]
    public void Setup()
    {
        //_driver = new ChromeDriver(); //pot sa lucrez cu orice fel de browser 
        _driver = new EdgeDriver();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        _driver.Manage().Window.Maximize();
    }


    [Test]
    [Category("ddt")]
    [TestCaseSource(nameof(Login))] //inainte (LoginModelDataSource))]
    public void TestWithPOMUsingFluentAssertions(LoginModel loginModel)
    {

        //POM initialization
        //Arrange
        LoginPage loginPage = new LoginPage(_driver);

        //Act
        loginPage.ClickLogin();
        loginPage.Login(loginModel.UserName, loginModel.Password);

        //Assert
        var getLoggedIn = loginPage.IsLoggedIn();
        Assert.IsTrue(getLoggedIn.employeeDetails & getLoggedIn.manageUser);

        //getLoggedIn.employeeDetails.Should().BeTrue(); //FluentASsertions
        // Assert.IsTrue(getLoggedIn.employeeDetails & getLoggedIn.manageUser); => assert simplu
    }

    //[Test]
    //[Category("ddt")]
    //public void TestWithPOMWithJsonData()
    //{

    //    string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "login.json");
    //    var jsonString = File.ReadAllText(jsonFilePath); //si citest path-ul json ptr jsonString

    //    var loginModel = JsonSerializer.Deserialize<LoginModel>(jsonString);

    //    //POM initialization
    //    LoginPage loginPage = new LoginPage(_driver);
    //    loginPage.ClickLogin();
    //    loginPage.Login(loginModel.Username, loginModel.Password);
    //}

    //metoda utilizeaza TestSource cu IEnumerable
    public static IEnumerable<LoginModel> Login()
    {
        yield return new LoginModel()
        {
            UserName = "admin",
            Password = "password"
        };
        //yield return new LoginModel()
        //{
        //    UserName = "admin2",
        //    Password = "password"
        //};
        //yield return new LoginModel()
        //{
        //    UserName = "admin3",
        //    Password = "password"
        //};

        //LE-AM COMENTAT, NU ERAU ININTE
        //string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "login.json");
        //var jsonString = File.ReadAllText(jsonFilePath);

        //var loginModel = JsonSerializer.Deserialize<List<LoginModel>>(jsonString);
        ////va citi toate listele de date, ruleaza prin iteratie si returneaza fiecar valoare singura
        ////una cate una de fiecare data odata ce DataSource va fi numita in metoda POM MAI sus
        //foreach (var loginData in loginModel)
        //{
        //    yield return loginData;
        //}
    }

    public static IEnumerable<LoginModel> LoginModelDataSource() 
    {
        string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "login.json");
        var jsonString = File.ReadAllText(jsonFilePath);

        var loginModel = JsonSerializer.Deserialize<List<LoginModel>>(jsonString);
        //va citi toate listele de date, ruleaza prin iteratie si returneaza fiecar valoare singura
        //una cate una de fiecare data odata ce DataSource va fi numita in metoda POM MAI sus
        foreach (var loginData in loginModel)
        {
            yield return loginData;
        }
    }

    private void ReadJsonFile()
    {
        //1.combine path for the app current domain cu  "login.json"
        string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "login.json");
        var jsonString = File.ReadAllText(jsonFilePath); //si citest path-ul json ptr jsonString

        //2.deserialize the jsonString to <LoginModel>
        //parseaza json file si converteste jsonString la <LoginModel>
        var loginModel = JsonSerializer.Deserialize<LoginModel>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }); // jsonString este continutul fisierului

        Console.WriteLine($"UserName: {loginModel.UserName} Password: {loginModel.Password}");
    }


    [TearDown]
    public void TearDown()
    {
        _driver?.Dispose();
    }

}
