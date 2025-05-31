Feature: Position
An admin user can create, edit, activate and deactivate and delete a position.

@positive
Scenario: Create a position
	Given I log in as AdminUser
	And I open the Admin/Position/All page
	And I click the add button
