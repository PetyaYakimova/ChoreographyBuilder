Feature: Users
An admin user can view and search users.

Background:
	Given I log in as AdminUser
	And I open the Admin/User/All page

@positive
Scenario: View users table and search in it
	Then assert that the table has at least 3 rows
	And assert that the table has columns with names Email, Figures, Verse Choreographies, Full Choreographies
	And assert that row with first.user@auto.test is visible in the table
	And assert that row with second.user@auto.test is visible in the table
	When I search in the table by first.user search term in the SearchTerm search field
	Then assert that the table has at least 1 rows
	And assert that the first user in the table has email first.user@auto.test, at least 4 figures, at least 3 verse choreographies, at least 1 full choreographies
