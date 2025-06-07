using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class PositionPage : BasePage
{
    public PositionPage(AppSettings settings, IWebDriver driver, WebDriverWait wait) : base(settings, driver, wait)
    {
    }

    private IWebElement AddPage_NameField => driver.FindElement(AddPage_NameFieldBy);
    private By AddPage_NameFieldBy => By.Id("Name");

    public void FillNameField(string name)
    {
        AddPage_NameField.Clear();
        AddPage_NameField.SendKeys(name);
    }
}
