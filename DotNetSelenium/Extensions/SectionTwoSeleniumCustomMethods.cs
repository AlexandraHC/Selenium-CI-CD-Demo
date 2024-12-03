//using OpenQA.Selenium; => am refactorizat
namespace DotNetSelenium.Extensions;

public static class SectionTwoSeleniumCustomMethods
{
    //Metoda statica, folosita in continuare ptr. metode (nu are corp, ii fac implementare in clasa de test de la
    //cursul 9)
    //| passez driverul de la cursul 9 din finalul metodei
    // driver este obiectul, si apoi
    //si apoi locatorul By
    //1.Method should get the locator 
    //2.Start getting the type of identifier
    //3.Perform operation on the locator
    public static void ClickElement(this IWebElement locator) //locatorul fiind -> By.Id("loginLink"))
    {
        //ii passes locatorul, folosind actiunea de click ma folosesc de motdoda Click() 
        locator.Click();
    }
    public static void SubmitElement(this IWebElement locator)
    {
        locator.Submit();
    }

    //Facand metoda de mai sus o pot folosi in clasa de test
    //in metodat de jos am incapsulat o metoda pe care o folosesc in metoda de test 
    //ptr a reduce nr de linii de cod - > ptr punctul 4 
    public static void EnterText(this IWebElement locator, string text)
    {
        locator.Clear();
        locator.SendKeys(text);
    }
    //o alta metoda ptr dropdown din cursul 8 
    //metode pe care le folosim ca sa simplificam codul si sa il reducem, sa fie mai usor de citit
    public static void SelectDropdownByText(IWebElement locator, string text)
    {
        SelectElement selectElement = new SelectElement(locator);
        selectElement.SelectByText(text);
    }

    public static void SelectDropdownByValue(this IWebElement locator, string value)
    {
        var selectElement = new SelectElement(locator);
        selectElement.SelectByText(value);
    }

    //CURS 10 -contiunare custom methods partea 2-a
    public static void MultiSelectElements(this IWebElement locator, string[] values)
    {
        var multiSelect = new SelectElement(locator);

        foreach (var value in values)
        {
            multiSelect.SelectByValue(value);
        }
    }

    public static List<string> GetAllSelectedList(this IWebElement locator)
    {
        var options = new List<string>();

        var multiSelect = new SelectElement(locator);

        var selectedOptions = multiSelect.AllSelectedOptions; /*IList<IWebElement>  (folosesc var)*/
        //itereaza prin colectia de mai sus ptr toate optiunile selectate
        foreach (IWebElement option in selectedOptions)
        {
            options.Add(option.Text);
        }
        return options;
    }
}
