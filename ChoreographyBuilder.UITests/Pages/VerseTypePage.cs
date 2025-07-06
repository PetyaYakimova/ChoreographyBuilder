using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.UITests.Models;
using ChoreographyBuilder.UITests.Repositories;
using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class VerseTypePage : BasePage
{
    private VerseTypeRepository verseTypeRepository;

    public VerseTypePage(AppSettings settings, IWebDriver driver, WebDriverWait wait, VerseTypeRepository verseTypeRepository) : base(settings, driver, wait)
    {
        this.verseTypeRepository = verseTypeRepository;
    }

    public VerseType? GetVerseTypeFromDbByName(string name)
        => verseTypeRepository.GetVerseTypeByName(name);

    public List<PositionFromTableModel> GetPositionsFromTable()
    {
        IEnumerable<List<string>> tableData = GetTableRowsData();
        List<PositionFromTableModel> positions = tableData.Select(row =>
            new PositionFromTableModel()
            {
                Name = row[0],
            })
            .ToList();

        return positions;
    }
}
