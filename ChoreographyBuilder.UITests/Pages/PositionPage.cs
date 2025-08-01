﻿using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.UITests.Models;
using ChoreographyBuilder.UITests.Repositories;
using ChoreographyBuilder.UITests.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ChoreographyBuilder.UITests.Pages;

public class PositionPage : BasePage
{
    private PositionRepository positionRepository;

    public PositionPage(AppSettings settings, IWebDriver driver, WebDriverWait wait, PositionRepository positionRepository) : base(settings, driver, wait)
    {
        this.positionRepository = positionRepository;
    }

    public Position? GetPositionFromDbByName(string name)
        => positionRepository.GetPositionByName(name);

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
