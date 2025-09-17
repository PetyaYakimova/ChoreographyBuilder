using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.UITests.Repositories;
using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class FigurePage : BasePage
{
    private FigureRepository figureRepository;

    public FigurePage(AppSettings settings, IWebDriver driver, WebDriverWait wait, FigureRepository figureRepository) : base(settings, driver, wait)
    {
        this.figureRepository = figureRepository;
    }

    public Figure? GetFigureFromDbByName(string name)
        => figureRepository.GetFigureByName(name);
}
