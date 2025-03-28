using BoDi;
using ChoreographyBuilder.UITests.Repositories;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.Setup;

[Binding]
public class SetUpHook
{
    private readonly IConfigurationRoot configuration;
    private WebDriverWait wait;
    private readonly AppSettings settings;
    private SeedDataRepository seedDataRepository;

    public SetUpHook(IObjectContainer objectContainer)
    {
        configuration = BuildConfiguration();
        settings = configuration.Get<AppSettings>();
        objectContainer.RegisterInstanceAs(this.settings);
    }

    [BeforeScenario]
    public void Setup(IObjectContainer objectContainer, ScenarioContext scenarioContext)
    {
        IWebDriver driver = this.GetDriver();

        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        driver.Manage().Window.Maximize();
        driver.Manage().Cookies.DeleteAllCookies();

        this.wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
        objectContainer.RegisterInstanceAs(driver);
        objectContainer.RegisterInstanceAs(wait);

        this.seedDataRepository = new SeedDataRepository(this.settings);
        seedDataRepository.SeedInitialUsersData();
    }

    [AfterScenario]
    public void TearDown(IObjectContainer objectContainer)
    {
        seedDataRepository.DeleteSeededData();

        IWebDriver driver = objectContainer.Resolve<IWebDriver>();
        driver.Quit();
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        ConfigurationBuilder builder = new();

#if DEBUG
        builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Debug.json"), optional: false, reloadOnChange: false);
#endif

        return builder.Build();
    }

    private IWebDriver GetDriver()
    {
        string browserName = Environment.GetEnvironmentVariable("browser") ?? "chrome";

        switch (browserName.ToLower())
        {
            case "chrome":
                ChromeOptions options = new ChromeOptions();
                //options.AddArgument("--headless");
                options.AddArgument("--enable-automation");
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                IWebDriver driver = new ChromeDriver(".", options);

                return driver;

            case "firefox":
                FirefoxOptions firefoxOptions = new FirefoxOptions();
                //firefoxOptions.AddArgument("--headless");
                firefoxOptions.AddArgument("--enable-automation");
                firefoxOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                IWebDriver firefoxDriver = new FirefoxDriver(".", firefoxOptions);

                return firefoxDriver;

            default:
                throw new ArgumentException($"Browser {browserName} is not supported.");
        }
    }
}

