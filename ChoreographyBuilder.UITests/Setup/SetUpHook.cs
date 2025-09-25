using BoDi;
using ChoreographyBuilder.UITests.Repositories;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace ChoreographyBuilder.UITests.Setup;

[Binding]
public class SetUpHook
{
    private static IConfigurationRoot configuration;
    private static AppSettings settings;
    private WebDriverWait wait;

    public SetUpHook(IObjectContainer objectContainer)
    {
        if (configuration == null)
        {
            configuration = BuildConfiguration();
            settings = configuration.Get<AppSettings>();
        }
        objectContainer.RegisterInstanceAs(settings);
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        if (configuration == null)
        {
            configuration = BuildConfiguration();
            settings = configuration.Get<AppSettings>();
        }

        try
        {
            ManageDataRepository manageDataRepository = new ManageDataRepository(settings, new Credentials());
            manageDataRepository.SeedInitialUsersData();
        }
        catch (Exception e)
        {
            Assert.Fail($"The manage data repository couldn't be created: {e.InnerException}");
        }
    }

    [BeforeScenario]
    public void Setup(IObjectContainer objectContainer, ScenarioContext scenarioContext)
    {
        IWebDriver driver = this.GetDriver();

        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        driver.Manage().Window.Maximize();
        driver.Manage().Cookies.DeleteAllCookies();

        wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
        objectContainer.RegisterInstanceAs(driver);
        objectContainer.RegisterInstanceAs(wait);
    }

    [AfterScenario]
    public void TearDown(IObjectContainer objectContainer)
    {
        IWebDriver driver = objectContainer.Resolve<IWebDriver>();
        driver.Quit();
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        if (settings != null)
        {
            ManageDataRepository manageDataRepository = new ManageDataRepository(settings, new Credentials());
            manageDataRepository.DeleteAutomationData();
        }
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
                options.AddArgument("--ignore-certificate-errors");
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                IWebDriver driver = new ChromeDriver(".", options);

                return driver;

            case "firefox":
                FirefoxOptions firefoxOptions = new FirefoxOptions();
                //firefoxOptions.AddArgument("--headless");
                firefoxOptions.AddArgument("--enable-automation");
                firefoxOptions.AcceptInsecureCertificates = true;
                firefoxOptions.AddArgument("--no-sandbox");
                firefoxOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                IWebDriver firefoxDriver = new FirefoxDriver(".", firefoxOptions);

                return firefoxDriver;

            case "edge":
                EdgeOptions edgeOptions = new EdgeOptions();
                //edgeOptions.AddArgument("--headless");
                edgeOptions.AddArgument("--enable-automation");
                edgeOptions.AddArgument("--ignore-certificate-errors");
                edgeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                IWebDriver edgeDriver = new EdgeDriver(".", edgeOptions);

                return edgeDriver;

            default:
                throw new ArgumentException($"Browser {browserName} is not supported.");
        }
    }
}

