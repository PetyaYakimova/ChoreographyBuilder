using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class FormPage : BasePage
{
    public FormPage(AppSettings settings, IWebDriver driver, WebDriverWait wait) : base(settings, driver, wait)
    {
    }

    public void FillField(string fieldName, string value)
    {
        IWebElement field = driver.FindElement(By.Id(fieldName));
        field.Clear();
        field.SendKeys(value);
    }

    public void ClearField(string fieldName)
        => driver.FindElement(By.Id(fieldName)).Clear();

    public void ClickCheckbox(string checkboxName)
    {
        IWebElement checkbox = driver.FindElement(By.Id(checkboxName));
        if (!checkbox.Selected)
        {
            checkbox.Click();
        }
    }

    public string GetValidationErrorMessage(string fieldName)
    {
        var errorElement = driver.FindElement(By.Id($"{fieldName}-error"));
        return errorElement.Text;
    }
}
