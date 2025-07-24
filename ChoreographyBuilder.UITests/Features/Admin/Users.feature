Feature: Users
An admin user can view and search users.

Background:
	Given I log in as AdminUser
	And I open the Admin/User/All page

@positive
Scenario: View users table and search in it
	Given [context]
	When [action]
	Then [outcome]
