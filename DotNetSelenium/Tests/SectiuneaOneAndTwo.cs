using DotNetSelenium.Pages__Sectiunea_3_;
//using OpenQA.Selenium; => //am taiat using-ul ptr a refactoriza punand-o in GlobalUsing class
//using OpenQA.Selenium.Chrome; => la fel si aici

namespace DotNetSelenium.Tests;

public class SectiuneaOneAndTwo
{
    [SetUp]         // = >>  are deja un atribute la creere proiect nou
    public void Setup()
    {
    }

    [Test]          //metoda de test, //CURS #3
    public void TestMhodOnGoogleWebsite()
    {
        // Sudo code for setting up Selenium
        //1. Create a new instance of Selenium Web Driver
        IWebDriver driver = new ChromeDriver();

        //2. Navigate to the URL
        driver.Navigate().GoToUrl("https://www.google.com/");

        //2.a. Maximize the browser window
        driver.Manage().Window.Maximize();

        //2.b Accept cookies dand clicl pe accepta cookie-uri
        var acceptCookiesButton = driver.FindElement(By.ClassName("sy4vM"));
        acceptCookiesButton.Click();

        //3. Find the element
        IWebElement webElemet = driver.FindElement(By.Name("q"));

        //4. Type in the element
        webElemet.SendKeys("Selenium");

        //5. Click on the element
        //ca si cum am dat enter  
        webElemet.SendKeys(Keys.Return); //sendkeys (are o proprietate -> Keys, practic asa dau enter)
    }


    //Curs #6
    [Test]  // methoda de test cu atributul [Test] 
    public void EAWebSiteTest()
    {
        //1Create a new instance of Selenium Web Driver
        var driver = new ChromeDriver();  //am pus var driver in loc de IWebDriver driver
        //2. Navigate to the URL
        driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        //3.Click the Login
        IWebElement loginLink = driver.FindElement(By.Id("loginLink"));
        //mai pot identifica si dupa linkText:
        //IWebElement loginLink = driver.FindElement(By.LinkText("Login"));
        //4.Click the Login link
        loginLink.Click();


        //EXPLICIT Wait ptr. user
        WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
        {
            PollingInterval = TimeSpan.FromMilliseconds(200),
            Message = "Textbox UserName does not appear during that timeframe"
        };

        driverWait.IgnoreExceptionTypes(typeof(NoSuchElementException));


        var txtUserName = driverWait.Until(d => 
        {
            var element = driver.FindElement(By.Name("UserNamesss"));
            return (element != null && element.Displayed) ? element : null;
        });

        //  |>  am comentat pt ca am elementul declarat in explicit wait method

        //5. Find the User Name text box
        //var txtUserName = driver.FindElement(By.Name("UserNamesss"));  //am pus=> var txtUserName in loc de  IWebElement txtUserName
       

        //6.Typing on the textUserName
        txtUserName.SendKeys("admin");
        //Curs #7 continuare de la curs 6 
        //7. Find the Password text box
        IWebElement txtPassword = driver.FindElement(By.Id("Password"));
        //8.Typing on the textPassword
        txtPassword.SendKeys("password");
        //9.Identify the Login Button
        //IWebElement btnLogin = driver.FindElement(By.ClassName("btn"));
        //9B.Identify the Login Button using CssSelector
        IWebElement btnLogin = driver.FindElement(By.CssSelector(".btn"));
        //10.Click Login button
        btnLogin.Submit();
    }

    [Test] //CURS 12 - INITIALIZAREA MODELULUI PAGINII DE                                                                                                                                                                                                                                                                                                               
    public void TestWithPOM()
    {
        //1Create a new instance of Selenium Web Driver
        var driver = new ChromeDriver();  //am pus var driver in loc de IWebDriver driver
        //2. Navigate to the URL
        driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        //POM initialization
        LoginPage loginPage = new LoginPage(driver); //am passat din constructor parametrul, cand am initializat driverul
                                                     //cu (IWebDriver driver) -> din LogiPage
        loginPage.ClickLogin();

        loginPage.Login("username", "password");


    }

    //COMENTEZ ACESTE METODE PT CA COMPILERUL MEU SE PLANGE DEOARECE CODUL TREBUIE REFACTORIZAT 
    //(FACAND MODELUL PAGINII DE OBIECTE SI CUSTOMIZAND METODELE  = > COMPILERUL SE VA PLANGE SI CODUL TB RESCRIS)

    ////Curs #7
    //[Test]  // methoda de test cu atributul [Test]  , am redus nr de linii de cod , in loc de 10 linii am scris 6 linii                                                                                                                                                                                                                                                                                                                                                             
    //public void EAWebSiteTestReduceSizeCode()
    //{
    //       //1Create a new instance of Selenium Web Driver
    //    IWebDriver driver = new ChromeDriver();
    //      //2. Navigate to the URL
    //    driver.Navigate().GoToUrl("http://eaapp.somee.com/");
    //      //3.Find and Click the Login link
    //    SectionTwoSeleniumCustomMethods.Click(driver, By.Id("loginLink")); //met. facuta la curs 9 mai jos
    //      //mai pot identifica si dupa linkText:
    //     //IWebElement loginLink = driver.FindElement(By.LinkText("Login"));

    //     //4. Find the User Name text box
    //        // in loc de varianta asta folosesc metodata de mai jos: driver.FindElement(By.Name("UserName")).SendKeys("admin");
    //    SectionTwoSeleniumCustomMethods.EnterText(driver, (By.Name("UserName")), "admin");
    //     //5. Find the Password text box
    //    SectionTwoSeleniumCustomMethods.EnterText(driver, (By.Name("Password")), "password");
    //      //6.Identify the Login Button using CssSelector
    //    driver.FindElement(By.CssSelector(".btn")).Submit(); ;
    //}

    //#8 - Curs 8: Interracting with DropDown and MultiSelect Option using Selenium

    //[Test]  // methoda de test cu atributul [Test]  
    //public void WorkingWithAdvancedControls()
    //{
    //    //1Create a new instance of Selenium Web Driver
    //    IWebDriver driver = new ChromeDriver();

    //    //2.a) Navigate to the URL
    //    driver.Navigate().GoToUrl("file:///C:/testpage.html");


    //    //select element este folosit pentru dropdown - uri
    //    //dropdown-ul este compus din 2 elemente (proprietati): valoare si text 
    //    //textul este ceea ce vezi dar fiecare optiune din asta are si un fel de id , o valoare
    //    //aia nu o vad , este pusa in html

    //    //3. Select an option from the dropdown
    //    //SelectElement selectElement = new SelectElement(driver.FindElement(By.Id("dropdown")));
    //    //selectElement.SelectByText("Option 3");
    //    SectionTwoSeleniumCustomMethods.SelectDropdownByText(driver, By.Id("dropdown"), "Option 2");

    //    //Curs 10 , cum sa reduc cele 6 linii de cod de mai jos printr-o met. personalizate(custom method)
    //    //SelectElement multiselect = new SelectElement(driver.FindElement(By.Id("dropdown")));
    //    //multiselect.SelectByValue("multi1");
    //    //multiselect.SelectByValue("multi2");

    //    SectionTwoSeleniumCustomMethods.MultiSelectElements(driver, By.Id("multiselect"), [ "multi1", "multi1" ]);

    //    //ptr liniile de cod de mai jos am folosit: in alata clasa metoda: public static List<string> GetAllSelectedList(IWebDriver driver, By locator)
    //    //IList<IWebElement> selectedOptions = multiselect.AllSelectedOptions;
    //    ////itereaza prin colectia de mai sus ptr toate optiunile selectate
    //    //foreach (IWebElement option in selectedOptions)
    //    //{
    //    //    Console.WriteLine(option.Text); //afiseaza toate valorile selectate
    //    //}

    //    var getSelectedOptions = SectionTwoSeleniumCustomMethods.GetAllSelectedList(driver, By.Id("multiselect"));
    //    //in loc de  List<string> pot sa folosesc => var
    //    getSelectedOptions.ForEach(Console.WriteLine);

    //    //CURS 9 continuare tot pe aceeasi metoda - Writing Custom Methods for Selenium UI Elements
    //    //in loc de cele 2 linii de cod voi folosi 1 singura
    //    IWebElement loginLink = driver.FindElement(By.Id("loginLink"));
    //    //4. Click on the Login link
    //    loginLink.Click();

    //    //in loc de cele 2 linii de codde deasupra am folosit metoda statica Click() facuta in 
    //    //clasa->SectionTwoSeleniumCustomMethods
    //    SectionTwoSeleniumCustomMethods.Click(driver, By.Id("loginLink"));


    //    //Should get the locator ()
    //    //2.Start getting the typeof identifier
    //    //3.Perform

    //}

    [Test]          //metoda de test, //CURS #16, Sectiunea:4
    public void Test4()
    {
    }

    [Test]
    public void Test5()
    {
    }

    //Nepharma website ptr, proiect,  tot pentru cursul 8 
    //metoda: public void WorkingWithAdvancedControls() {} 
    [Test]  // methoda de test cu atributul [Test]
    public void WorkingWithAdvancedControlsOnNewpharmaWebsite()
    {
        //1Create a new instance of Selenium Web Driver
        IWebDriver driver = new ChromeDriver();

        //2.a) Navigate to the URL
        driver.Navigate().GoToUrl("https://www.newpharma.be/pharmacie/cat/beaute-cosmetiques/12.html");

        //2.b) Accet cookies
        driver.FindElement(By.ClassName("js-cookie-policy-ok-btn")).Click();

        driver.FindElement(By.ClassName("bht-menu-category-2")).Click();

        var dropdown = new SelectElement(driver.FindElement(By.ClassName("bht-select-sort-by")));
        dropdown.SelectByText(" Prix croissant ");


        //3.Click on the dropdown:
        driver.FindElement(By.CssSelector(".btn")).Click();

        SelectElement selectElement = new SelectElement(driver.FindElement(By.Id("dropdown")));
        selectElement.SelectByText("Option 2");

    }


}