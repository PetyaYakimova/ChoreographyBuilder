using ChoreographyBuilder.UITests.Helpers;
using ChoreographyBuilder.UITests.Models;

namespace ChoreographyBuilder.UITests.Setup;

public class Credentials
{
    public UserModel FirstUser()
    {
        UserModel userModel = new UserModel()
        {
            Email = "first.user" + TestConstants.AutomationMailEnding,
            Password = "firstUser123!",
        };

        return userModel;
    }

    public UserModel SecondUser()
    {
        UserModel userModel = new UserModel()
        {
            Email = "second.user" + TestConstants.AutomationMailEnding,
            Password = "secondUser123456",
        };

        return userModel;
    }

    public UserModel AdminUser()
    {
        UserModel userModel = new UserModel()
        {
            Email = "admin.user" + TestConstants.AutomationMailEnding,
            Password = "Admin987",
        };

        return userModel;
    }
}
