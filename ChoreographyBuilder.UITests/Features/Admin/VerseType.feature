Feature: VerseType
An admin user can create, edit, activate and deactivate and delete a verse type.

Background:
	Given I log in as AdminUser
	And I open the Admin/VerseType/All page

@positive
Scenario: View verse type table and search in it
	Then assert that the table has at least 2 rows
	And assert that the table has columns with names Name, Beats Count, Active, Actions
	And assert that the first row in the table has values for Name, Beats Count, Active and Actions columns
	When I search in the table by AutoTest verse 1 search term
	Then assert that the table has at least 1 rows
	And assert that the first verse type in the table has name AutoTest verse 1 and beats count 32
	When I clear the SearchTerm field

@positive
Scenario: Create verse type
	Given I click add button
	When I fill the verse type form with name AutoTest123, beat counts 40
	And I click the Save button
	Then assert that I am on Admin/VerseType/All page
	And assert that I see toaster message with text The verse type has been added successfully.
	And I have asserted that a verse type with name AutoTest123, beats count 40, that is active exists
	When I search in the table by AutoTest123 search term
	Then assert that the table has at least 1 rows
	And assert that the first verse type in the table has name AutoTest123 and beats count 40

@negative
Scenario: Create verse type with invalid data
	Given I click add button
	When I fill the Name field with AB
	And I click the Save button
	Then assert that I am on Admin/VerseType/Add page
	And assert that I see validation error message for Name field with text The Name field must be between 3 and 20 characters long.
	When I clear the Name field
	And I click the Save button
	Then assert that I am on Admin/VerseType/Add page
	And assert that I see validation error message for Name field with text The Name field is required.
	When I fill the Name field with AutoTest12AutoTest123
	And I fill the BeatCounts field with 1
	And I click the Save button
	Then assert that I am on Admin/VerseType/Add page
	And assert that I see validation error message for BeatCounts field with text The number must be between 2 and 120.
	When I clear the BeatCounts field
	And I click the Save button
	Then assert that I am on Admin/VerseType/Add page
	And assert that I see validation error message for BeatCounts field with text The Beats Count field is required.
	When I fill the BeatCounts field with 122
	And I click the Save button
	Then assert that I am on Admin/VerseType/Add page
	And assert that I see validation error message for BeatCounts field with text The number must be between 2 and 120.
	When I fill the BeatCounts field with 15
	And I click the Save button
	Then assert that I am on Admin/VerseType/Add page
	And assert that I see validation error message for BeatCounts field with text The number must be even.
	When I fill the BeatCounts field with 120
	And I click the Save button
	Then assert that I am on Admin/VerseType/All page
	And assert that I see toaster message with text The verse type has been added successfully.
	And I have asserted that a verse type with name AutoTest12AutoTest12, beats count 120, that is active exists

@positive
Scenario: Deactivate verse type
	Given I search in the table by AutoTest active search term
	When I click the Deactivate button
	Then assert that I am on Admin/VerseType/All page
	And assert that I see toaster message with text The status of the verse type has been successfully changed.
	And I have asserted that a verse type with name AutoTest active, beats count 44, that is not active exists

@positive
Scenario: Activate verse type
	Given I search in the table by AutoTest inactive search term
	When I click the Activate button
	Then assert that I am on Admin/VerseType/All page
	And assert that I see toaster message with text The status of the verse type has been successfully changed.
	And I have asserted that a verse type with name AutoTest inactive, beats count 16, that is active exists

@positive
Scenario: Edit verse type
	Given I search in the table by AutoTest for edit search term
	When I click the Edit button
	And I fill the verse type form with name AutoTest edited, beat counts 46
	And I click the Save button
	Then assert that I see toaster message with text The verse type has been updated successfully.
	And assert that I am on Admin/VerseType/All page
	And I have asserted that a verse type with name AutoTest edited, beats count 46, that is active exists

@positive
Scenario: Edit verse type with invalid data
	Given I search in the table by edit search term
	When I click the Edit button
	And I clear the Name field
	And I click the Save button
	Then assert that I see validation error message for Name field with text The Name field is required.
	When I fill the Name field with AutoTest edited
	And I clear the BeatCounts field
	And I click the Save button
	Then assert that I see validation error message for BeatCounts field with text The Beats Count field is required.
	When I fill the BeatCounts field with 45
	And I click the Save button
	Then assert that I see validation error message for BeatCounts field with text The number must be even.

@positive
Scenario: Delete verse type
	Given I search in the table by AutoTest for delete search term
	When I click the Delete button
	And I click the Delete button
	Then assert that I see toaster message with text The verse type has been deleted.
	And assert that I am on Admin/VerseType/All page