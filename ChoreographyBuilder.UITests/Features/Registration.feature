Feature: Registration
A user can register with valid data.

@positive
Scenario: Register a user with valid data
	Given I open the Identity/Account/Register page
	And I fill the registration form with email register_user@auto.test, password SomePass123, confirm password SomePass123
	When I click the Register button
	Then assert that I see email register_user@auto.test in the header
	And assert that I am on Home/Stats page
	And I have asserted that a new user with email register_user@auto.test is saved