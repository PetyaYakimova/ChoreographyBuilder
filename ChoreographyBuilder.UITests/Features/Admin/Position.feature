Feature: Position
An admin user can create, edit, activate and deactivate and delete a position.

@positive
Scenario: Create a position
	Given I log in as AdminUser
	And I open the Admin/Position/All page
	And I click the add button
	When I fill the name field for position with AutoTest123
	And I click the save button