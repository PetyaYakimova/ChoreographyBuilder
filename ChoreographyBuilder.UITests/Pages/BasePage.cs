using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
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

    protected IWebElement Table_SearchInputField => driver.FindElement(Table_SearchInputFieldBy);
    protected By Table_SearchInputFieldBy => By.Id("SearchTerm");

    protected IWebElement Table_Row => driver.FindElement(Table_RowBy);
    protected By Table_RowBy => By.XPath("//tbody//tr");

    protected IWebElement Table_DeactivateButton => driver.FindElement(Table_DeactivateButtonBy);
    protected By Table_DeactivateButtonBy => By.XPath("//input[@value='Deactivate']");

    protected IWebElement Table_ActivateButton => driver.FindElement(Table_ActivateButtonBy);
    protected By Table_ActivateButtonBy => By.XPath("//input[@value='Activate']");

    protected IWebElement AddButton => driver.FindElement(AddButtonBy);
    protected By AddButtonBy => By.Id("add-action");

    protected IWebElement SaveButton => driver.FindElement(SaveButtonBy);
    protected By SaveButtonBy => By.Id("save-action");

    protected IWebElement ToasterMessage => driver.FindElement(ToasterMessageBy);
    //protected By ToasterMessageBy => By.XPath("//*[@id='toast-container']//div[@class='toast-message']");
    protected By ToasterMessageBy => By.ClassName("toast-message");

    public void OpenHomePage()
        => driver.Navigate().GoToUrl(settings.DomainSettings.Domain);

    public void OpenPage(string pageName)
        => driver.Navigate().GoToUrl(settings.DomainSettings.Domain + pageName);

    public string GetCurrentURL()
        => driver.Url;

    public string GetCurrentPage()
        => GetCurrentURL().Replace(settings.DomainSettings.Domain, string.Empty);

    public string GetGreetingFromHeader()
        => HeaderGreetingText.Text;

    public List<string> GetHeaderMenus()
        => driver.FindElements(HederMenuItemBy).Select(e => e.Text).ToList();

    public void ClickOnMenu(string menuName)
        => driver.FindElement(By.XPath($"//header//a[contains(text(), '{menuName}')]")).Click();

    public void ClickOnSiteLogo()
        => HederLogo.Click();

    public void ClickAddButton()
        => AddButton.Click();

    public void ClickSaveButton()
        => SaveButton.Click();

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

    // Tables
    #region
    public void SearchInTableBySearchTerm(string searchTerm)
    {
        Table_SearchInputField.Clear();
        Table_SearchInputField.SendKeys(searchTerm);
        Table_SearchInputField.SendKeys(Keys.Enter);
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

    public void ClickDeactivateButtonForFirstRecordInTable()
        => Table_DeactivateButton.Click();

    public void ClickActivateButtonForFirstRecordInTable()
        => Table_ActivateButton.Click();
    #endregion

    //Forms
    #region
    public string GetValidationErrorMessage(string fieldName)
    {
        var errorElement = driver.FindElement(By.Id($"{fieldName}-error"));
        return errorElement.Text;
    }
    #endregion
}
