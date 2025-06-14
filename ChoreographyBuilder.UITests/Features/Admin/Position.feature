Feature: Position
An admin user can create, edit, activate and deactivate and delete a position.

@positive
Scenario: Create a position
	Given I log in as AdminUser
	And I open the Admin/Position/All page
	And I click the add button
	When I fill the name field for position with AutoTest123
	And I click the save button
	Then assert that I am on Admin/Position/All page
	And I have asserted that a position with name AutoTest123 that is active exists
	When I search in the table by AutoTest123
	Then assert that the table has at least 1 rows
	# Add steps to check the data for the first row in the table