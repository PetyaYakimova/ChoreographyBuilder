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

    public List<VerseTypeFromTableModel> GetVerseTypesFromTable()
    {
        IEnumerable<List<string>> tableData = GetTableRowsData();
        List<VerseTypeFromTableModel> verseTypes = tableData.Select(row =>
            new VerseTypeFromTableModel()
            {
                Name = row[0],
                BeatCounts = row[1],
            })
            .ToList();

        return verseTypes;
    }
}
