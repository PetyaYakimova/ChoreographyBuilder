Feature: Navigation
All kinds of users can navigate in the app.

@positive
Scenario: Admin user can navigate through the menus
	When I log in as AdminUser
	Then assert that the menus I see in the header are Stats, Users, Positions, Verse Types
	And assert that I am on Admin/Home/Stats page
