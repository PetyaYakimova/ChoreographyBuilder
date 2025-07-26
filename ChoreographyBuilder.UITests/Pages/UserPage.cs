using ChoreographyBuilder.UITests.Models;
using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class UserPage : BasePage
{
    public UserPage(AppSettings settings, IWebDriver driver, WebDriverWait wait) : base(settings, driver, wait)
    {
    }

    public List<UserFromTableModel> GetUsersFromTable()
    {
        IEnumerable<List<string>> tableData = GetTableRowsData();
        List<UserFromTableModel> users = tableData.Select(row =>
            new UserFromTableModel()
            {
                Email = row[0],
                Figures = int.Parse(row[1]),
                VerseChoreographies = int.Parse(row[2]),
                FullChoreographies = int.Parse(row[3]),
            })
            .ToList();

        return users;
    }
}
