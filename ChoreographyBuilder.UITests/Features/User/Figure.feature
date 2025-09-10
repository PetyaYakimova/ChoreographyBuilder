Feature: Figure
User can view, create, edit, copy and delete figures and their options

Background:
	Given I log in as FirstUser
	And I open the Figure/Mine page

@positive
Scenario: View figure table and search in it
	Then assert that the table has at least 2 rows
	And assert that the table has columns with names Name, Number of options, Highlight, Favourite, Shared
	And assert that row with First figure is visible in the table
	And assert that row with Second figure is visible in the table
	And assert that row with Highlight figure is visible in the table
	And assert that row with Fourth figure is not visible in the table
	When I search in the table by First figure search term in the SearchTerm search field
	Then assert that the table has at least 1 rows
	And assert that row with First figure is visible in the table
	And assert that row with Second figure is not visible in the table
	And assert that row with Highlight figure is not visible in the table
	When I clear search field SearchTerm
	Then assert that row with First figure is visible in the table
	And assert that row with Second figure is visible in the table
	And assert that row with Highlight figure is visible in the table
	When I search in the table by AutoTest position 1 dropdown option in StartPosition dropdown
	Then assert that row with First figure is visible in the table
	And assert that row with Second figure is visible in the table
	And assert that row with Highlight figure is not visible in the table
	When I search in the table by All dropdown option in StartPosition dropdown
	Then assert that row with First figure is visible in the table
	And assert that row with Second figure is visible in the table
	And assert that row with Highlight figure is visible in the table
	When I search in the table by AutoTest position 1 dropdown option in EndPosition dropdown
	Then assert that row with First figure is visible in the table
	And assert that row with Second figure is visible in the table
	And assert that row with Highlight figure is not visible in the table
	When I search in the table by All dropdown option in EndPosition dropdown
	Then assert that row with First figure is visible in the table
	And assert that row with Second figure is visible in the table
	And assert that row with Highlight figure is visible in the table
	When I search in the table by 16 search term in the BeatsCount search field
	Then assert that row with First figure is not visible in the table
	And assert that row with Second figure is not visible in the table
	And assert that row with Highlight figure is visible in the table
	When I clear search field BeatsCount
	Then assert that row with First figure is visible in the table
	And assert that row with Second figure is visible in the table
	And assert that row with Highlight figure is visible in the table
	When I search in the table by Fast dropdown option in DynamicsType dropdown
	Then assert that row with Second figure is visible in the table
	And assert that row with First figure is not visible in the table
	And assert that row with Highlight figure is not visible in the table

@positive
Scenario: Create figure
	Given I click add button
	When I fill the figure form with name AutoTest999, that is highlight, that is not favourite, that is not shared with other users
	And I click the Save button
	Then assert that I see toaster message with text The figure has been added successfully.
	And assert that I see element with text No options found by these search criteria! A figure cannot be used in a choreography if it doesn't have options. Please add options.

@negative
Scenario: Create figure with invalid data
	Given I click add button
	When I fill the Name field with AB
	And I click the Save button
	Then assert that I am on Admin/VerseType/Add page
	And assert that I see validation error message for Name field with text The Name field must be between 3 and 20 characters long.
	When I clear the Name field
	And I click the Save button