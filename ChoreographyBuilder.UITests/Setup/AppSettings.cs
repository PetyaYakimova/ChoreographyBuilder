namespace ChoreographyBuilder.UITests.Setup;

public class AppSettings
{
	public DomainSettings DomainSettings { get; set; } = null!;

	public ConnectionStrings ConnectionStrings { get; set; } = null!;
}

public class DomainSettings
{
	public string Domain { get; set; } = null!;
}

public class ConnectionStrings
{
	public string DBConnectionString { get; set; } = null!;
}
