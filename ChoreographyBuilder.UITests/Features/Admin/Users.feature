Feature: Users
An admin user can view and search users.

Background:
	Given I log in as AdminUser
	And I open the Admin/User/All page

@positive
Scenario: View users table and search in it
	Then assert that the table has at least 3 rows
	And assert that the table has columns with names Name, Beats Count
