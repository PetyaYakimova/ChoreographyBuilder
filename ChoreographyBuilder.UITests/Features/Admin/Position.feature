Feature: Position
An admin user can create, edit, activate and deactivate and delete a position.

Background:
	Given I log in as AdminUser
	And I open the Admin/Position/All page

@positive
Scenario: Create a position
	Given I click the add button
	When I fill the name field for position with AutoTest123
	And I click the save button
	Then assert that I am on Admin/Position/All page
	And I have asserted that a position with name AutoTest123 that is active exists
	When I search in the table by AutoTest123
	Then assert that the table has at least 1 rows
	And assert that the first position in the table has name AutoTest123

@negative
Scenario: Create a position with invalid data
	Given I click the add button
	When I fill the name field for position with A
	And I click the save button
	Then assert that I am on Admin/Position/Add page
	And assert that I see validation error message for Name field with text The Name field must be between 2 and 70 characters long.
	When I clear the name field for position
	And I click the save button
	Then assert that I am on Admin/Position/Add page
	And assert that I see validation error message for Name field with text The Name field is required.
	When I fill the name field for position with AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest123
	And I click the save button
	Then assert that I am on Admin/Position/All page
	And I have asserted that a position with name AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12 that is active exists

@positive
Scenario: Deactivate a position
	Given I search in the table by AutoTest active position
	When I click the deactivate button for the first record in the table
	Then assert that I am on Admin/Position/All page
	And assert that I see toaster message with text The status of the position has been successfully changed.
	And I have asserted that a position with name AutoTest active position that is not active exists

	# add tests for activate, edit and delete positions, add a negative test for trying to edit a position with invalid name