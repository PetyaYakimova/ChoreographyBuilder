using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.UITests.Repositories;
using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class FigurePage : BasePage
{
    private FigureRepository figureRepository;
    private FigureOptionRepository figureOptionRepository;

    public FigurePage(AppSettings settings, IWebDriver driver, WebDriverWait wait, FigureRepository figureRepository, FigureOptionRepository figureOptionRepository) : base(settings, driver, wait)
    {
        this.figureRepository = figureRepository;
        this.figureOptionRepository = figureOptionRepository;
    }

    public Figure? GetFigureFromDbByName(string name)
        => figureRepository.GetFigureByName(name);

    public FigureOption? GetFigureOptionFromDbByFigureNameAndBeatsCount(string figureName, int beatsCount)
        => figureOptionRepository.GetFigureOptionByFigureNameAndBeatsCount(figureName, beatsCount);
}
