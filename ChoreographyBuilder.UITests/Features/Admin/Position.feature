﻿Feature: Position
An admin user can view, create, edit, activate, deactivate, delete and search positions.

Background:
	Given I log in as AdminUser
	And I open the Admin/Position/All page

@positive
Scenario: View position table and search in it
	Then assert that the table has at least 2 rows
	And assert that the table has columns with names Name
	And assert that row with AutoTest position 1 is visible in the table
	And assert that row with AutoTest position 2 is visible in the table
	When I search in the table by AutoTest position 1 search term in the SearchTerm search field
	Then assert that the table has at least 1 rows
	And assert that the first position in the table has name AutoTest position 1
	And assert that row with AutoTest position 1 is visible in the table
	And assert that row with AutoTest position 2 is not visible in the table
	When I clear search field SearchTerm
	Then assert that row with AutoTest position 1 is visible in the table
	And assert that row with AutoTest position 2 is visible in the table

@positive
Scenario: Create position
	Given I click add button
	When I fill the position form with name AutoTest123
	And I click the Save button
	Then assert that I am on Admin/Position/All page
	And assert that I see toaster message with text The position has been added successfully.
	And I have asserted that a position with name AutoTest123 that is active exists
	When I search in the table by AutoTest123 search term in the SearchTerm search field
	Then assert that the table has at least 1 rows
	And assert that the first position in the table has name AutoTest123

@negative
Scenario: Create position with invalid data
	Given I click add button
	When I fill the position form with name A
	And I click the Save button
	Then assert that I am on Admin/Position/Add page
	And assert that I see validation error message for Name field with text The Name field must be between 2 and 70 characters long.
	When I clear the Name field
	And I click the Save button
	Then assert that I am on Admin/Position/Add page
	And assert that I see validation error message for Name field with text The Name field is required.
	When I fill the position form with name AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest123
	And I click the Save button
	Then assert that I am on Admin/Position/All page
	And I have asserted that a position with name AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12 that is active exists

@positive
Scenario: Deactivate position
	Given I search in the table by AutoTest active position search term in the SearchTerm search field
	When I click the Deactivate button
	Then assert that I am on Admin/Position/All page
	And assert that I see toaster message with text The status of the position has been successfully changed.
	And I have asserted that a position with name AutoTest active position that is not active exists

@positive
Scenario: Activate position
	Given I search in the table by AutoTest inactive position search term in the SearchTerm search field
	When I click the Activate button
	Then assert that I am on Admin/Position/All page
	And assert that I see toaster message with text The status of the position has been successfully changed.
	And I have asserted that a position with name AutoTest inactive position that is active exists

@positive
Scenario: Edit position
	Given I search in the table by AutoTest position for edit search term in the SearchTerm search field
	When I click the Edit button
	And I fill the position form with name AutoTest position edited
	And I click the Save button
	Then assert that I see toaster message with text The position has been updated successfully.
	And assert that I am on Admin/Position/All page
	And I have asserted that a position with name AutoTest position edited that is active exists

@positive
Scenario: Edit position with invalid data
	Given I search in the table by edit search term in the SearchTerm search field
	When I click the Edit button
	And I clear the Name field
	And I click the Save button
	Then assert that I see validation error message for Name field with text The Name field is required.

@positive
Scenario: Delete position
	Given I search in the table by AutoTest position for delete search term in the SearchTerm search field
	When I click the Delete button
	And I click the Delete button
	Then assert that I see toaster message with text The position has been deleted.
	And assert that I am on Admin/Position/All page