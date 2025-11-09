using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class BasePage
{
    protected readonly AppSettings settings;
    protected readonly IWebDriver driver;
    protected readonly WebDriverWait wait;

    protected BasePage(
        AppSettings settings,
        IWebDriver driver,
        WebDriverWait wait)
    {
        this.settings = settings;
        this.driver = driver;
        this.wait = wait;
    }

    protected IWebElement HeaderGreetingText => driver.FindElement(HeaderGreetingTextBy);
    protected By HeaderGreetingTextBy => By.Id("greeting");

    protected IWebElement HederMenuItem => driver.FindElement(HederMenuItemBy);
    protected By HederMenuItemBy => By.ClassName("nav-menu-item");

    protected IWebElement HederLogo => driver.FindElement(HederLogoBy);
    protected By HederLogoBy => By.ClassName("navbar-brand");

    protected IWebElement Table_SearchButton => driver.FindElement(Table_SearchButtonBy);
    protected By Table_SearchButtonBy => By.XPath("//input[@value='Search']");

    protected IWebElement Table_Row => driver.FindElement(Table_RowBy);
    protected By Table_RowBy => By.XPath("//tbody//tr");

    protected IWebElement AddButton => driver.FindElement(AddButtonBy);
    protected By AddButtonBy => By.Id("add-action");

    protected IWebElement ToasterMessage => driver.FindElement(ToasterMessageBy);
    protected By ToasterMessageBy => By.ClassName("toast-message");

    public void OpenHomePage()
        => driver.Navigate().GoToUrl(settings.DomainSettings.Domain);

    public void OpenPage(string pageName)
        => driver.Navigate().GoToUrl(settings.DomainSettings.Domain + pageName);

    public string GetCurrentURL()
        => driver.Url;

    public string GetCurrentPageWithoutParameters()
    {
        wait.Until(webDriver => ((IJavaScriptExecutor)webDriver)
            .ExecuteScript("return document.readyState").ToString().Equals("complete"));
        return GetCurrentURL().Replace(settings.DomainSettings.Domain, string.Empty).Split("?")[0];
    }

    public string GetGreetingFromHeader()
        => HeaderGreetingText.Text;

    public List<string> GetHeaderMenus()
        => driver.FindElements(HederMenuItemBy).Select(e => e.Text).ToList();

    public void ClickOnMenu(string menuName)
        => driver.FindElement(By.XPath($"//header//a[contains(text(), '{menuName}')]")).Click();

    public void ClickOnSiteLogo()
        => HederLogo.Click();

    public void ClickAddButton()
    {
        driver.ExecuteJavaScript("arguments[0].scrollIntoView(true);", AddButton);
        driver.ExecuteJavaScript("arguments[0].click();", AddButton);
    }

    public bool IsElementWithTextVisible(string text)
    {
        try
        {
            return driver.FindElement(By.XPath($"//*[contains(text(), '{text}')]")).Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    public bool DoesElementExistAndIsDisplayed(By locator)
    {
        try
        {
            return driver.FindElement(locator).Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    public string GetToasterMessage()
        => ToasterMessage.Text;

    public void ClickButtonWithValue(string buttonName)
    {
        driver.FindElement(By.XPath($"//input[@value='{buttonName}']")).Click();
        Thread.Sleep(200);
    }

    // Tables
    #region
    public void SearchInSearchFieldInTableBySearchTerm(string fieldName, string searchTerm)
    {
        var searchField = driver.FindElement(By.Id(fieldName));
        searchField.Clear();
        searchField.SendKeys(searchTerm);
        searchField.SendKeys(Keys.Enter);
        Thread.Sleep(1000); // Wait for the table to update after search
    }

    public void ClearSearchField(string fieldName)
    {
        var searchField = driver.FindElement(By.Id(fieldName));
        searchField.Clear();
        searchField.SendKeys(Keys.Enter);
        Thread.Sleep(1000); // Wait for the table to update after clearing search
    }

    public void SelectOptionInDropdownForSearch(string option, string dropdownName)
    {
        var dropdownField = driver.FindElement(By.Id(dropdownName));
        dropdownField.Click();

        driver.FindElement(By.XPath($"//*[@id='{dropdownName}']//option[contains(text(), '{option}')]")).Click();
        Table_SearchButton.Click(); // Click the search button to apply the filter
        Thread.Sleep(1000); // Wait for the table to update after search
    }

    public int GetNumberOfRowsInTable()
        => driver.FindElements(Table_RowBy).Count;

    public IEnumerable<List<string>> GetTableRowsData()
    {
        var rowsData = driver.FindElement(By.TagName("tbody"))
            .FindElements(By.TagName("tr"))
            .Select(e => e.FindElements(By.TagName("td"))
                .Where(el => el.Text != null)
                .Select(el => el.Text)
                .ToList());

        return rowsData;
    }

    public List<string> GetTableColumnNames()
    {
        var headerRow = driver.FindElement(By.TagName("thead"))
            .FindElement(By.TagName("tr"));

        return headerRow.FindElements(By.TagName("th"))
            .Where(el => !string.IsNullOrEmpty(el.Text))
            .Select(el => el.Text)
            .ToList();
    }

    public bool IsRowWithValueVisible(string value)
        => GetTableRowsData().Any(row => row.Any(cell => cell.Contains(value)));
    #endregion
}
