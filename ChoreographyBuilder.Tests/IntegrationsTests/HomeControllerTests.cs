using ChoreographyBuilder.Controllers;
using ChoreographyBuilder.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ChoreographyBuilder.Tests.IntegrationsTests;

[TestFixture]
public class HomeControllerTests
{
	private HomeController homeController;

	[OneTimeSetUp]
	public void OneTimeSetup()
	{
		this.homeController = new HomeController(UserServiceMock.Instance);
	}

	[Test]
	[TestCase(400)]
	[TestCase(401)]
	[TestCase(403)]
	[TestCase(404)]
	[TestCase(500)]
	public void Error_ShouldReturnView(int statusCode)
	{
		var result = this.homeController.Error(statusCode);

		Assert.IsNotNull(result);

		var viewResult = result as ViewResult;
		Assert.IsNotNull(viewResult);
	}

	[Test]
	public void Index_ShouldReturnViewForNonRegisteredUsers()
	{
		var result = this.homeController.Index();
		Assert.IsNotNull(result);

		var viewResult = result as ViewResult;
		Assert.IsNotNull(viewResult);
	}
}
