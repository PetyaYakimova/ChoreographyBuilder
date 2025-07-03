Feature: VerseType
An admin user can create, edit, activate and deactivate and delete a verse type.

Background:
	Given I log in as AdminUser
	And I open the Admin/VerseType/All page

@positive
Scenario: Create verse type
	Given I click add button
	When I fill the verse type form with name AutoTest123, beat counts 40
	And I click the Save button
	#Then assert that I am on Admin/Position/All page
	#And assert that I see toaster message with text The position has been added successfully.
	#And I have asserted that a position with name AutoTest123 that is active exists
	#When I search in the table by AutoTest123
	#Then assert that the table has at least 1 rows
	#And assert that the first position in the table has name AutoTest123

#@negative
#Scenario: Create position with invalid data
#	Given I click add button
#	When I fill the name field for position with A
#	And I click the Save button
#	Then assert that I am on Admin/Position/Add page
#	And assert that I see validation error message for Name field with text The Name field must be between 2 and 70 characters long.
#	When I clear the name field for position
#	And I click the Save button
#	Then assert that I am on Admin/Position/Add page
#	And assert that I see validation error message for Name field with text The Name field is required.
#	When I fill the name field for position with AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest123
#	And I click the Save button
#	Then assert that I am on Admin/Position/All page
#	And I have asserted that a position with name AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12 that is active exists
#
#@positive
#Scenario: Deactivate position
#	Given I search in the table by AutoTest active position
#	When I click the Deactivate button
#	Then assert that I am on Admin/Position/All page
#	And assert that I see toaster message with text The status of the position has been successfully changed.
#	And I have asserted that a position with name AutoTest active position that is not active exists
#
#@positive
#Scenario: Activate position
#	Given I search in the table by AutoTest inactive position
#	When I click the Activate button
#	Then assert that I am on Admin/Position/All page
#	And assert that I see toaster message with text The status of the position has been successfully changed.
#	And I have asserted that a position with name AutoTest inactive position that is active exists
#
#@positive
#Scenario: Edit position
#	Given I search in the table by AutoTest position for edit
#	When I click the Edit button
#	And I fill the name field for position with AutoTest position edited
#	And I click the Save button
#	Then assert that I see toaster message with text The position has been updated successfully.
#	And assert that I am on Admin/Position/All page
#	And I have asserted that a position with name AutoTest position edited that is active exists
#
#@positive
#Scenario: Edit position with invalid data
#	Given I search in the table by edit
#	When I click the Edit button
#	And I clear the name field for position
#	And I click the Save button
#	Then assert that I see validation error message for Name field with text The Name field is required.
#
#@positive
#Scenario: Delete position
#	Given I search in the table by AutoTest position for delete
#	When I click the Delete button
#	And I click the Delete button
#	Then assert that I see toaster message with text The position has been deleted.
#	And assert that I am on Admin/Position/All page