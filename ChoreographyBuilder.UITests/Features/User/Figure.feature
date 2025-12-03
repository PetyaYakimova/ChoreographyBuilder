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
	When I fill the figure form with name AutoTest999, that is highlight, that is favourite, that is not shared with other users
	And I click the Save button
	Then assert that I see toaster message with text The figure has been added successfully.
	And assert that I see element with text No options found by these search criteria!
	And I have asserted that a figure with name AutoTest999, that is highlight, that is favourite, that is not shared exists

@negative
Scenario: Create figure with invalid data
	Given I click add button
	When I fill the Name field with ABCD
	And I click the Save button
	Then assert that I am on Figure/Add page
	And assert that I see validation error message for Name field with text The Name field must be between 5 and 70 characters long.
	When I clear the Name field
	And I click the Save button
	Then assert that I am on Figure/Add page
	And assert that I see validation error message for Name field with text The Name field is required.
	When I fill the Name field with AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest123
	And I click the Save button
	Then assert that I see toaster message with text The figure has been added successfully.
	And I have asserted that a figure with name AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12AutoTest12, that is not highlight, that is not favourite, that is not shared exists

@positive
Scenario: Edit figure
	Given I search in the table by AutoTest for edit search term in the SearchTerm search field
	When I click the Edit button
	And I fill the figure form with name AutoTest88 edited, that is not highlight, that is not favourite, that is shared with other users
	And I click the Save button
	Then assert that I see toaster message with text The figure has been updated successfully.
	And assert that I am on Figure/Mine page
	And I have asserted that a figure with name AutoTest88 edited, that is not highlight, that is not favourite, that is shared exists

@negative
Scenario: Edit figure with invalid data
	Given I search in the table by edit search term in the SearchTerm search field
	When I click the Edit button
	And I clear the Name field
	And I click the Save button
	Then assert that I see validation error message for Name field with text The Name field is required.

@positive
Scenario: Delete figure
	Given I search in the table by AutoTest for delete search term in the SearchTerm search field
	When I click the Delete button
	And I click the Delete button
	Then assert that I see toaster message with text The figure has been deleted.
	And assert that I am on Figure/Mine page

@positive
Scenario: View figure options table and search in it
	Given I search in the table by Second figure search term in the SearchTerm search field
	When I click the Options button
	Then assert that the table has at least 3 rows
	And assert that the table has columns with names Start position, End position, Beats count, Dynamics type
	And assert that row with Regular is visible in the table
	And assert that row with Fast is visible in the table
	And assert that row with Slow is not visible in the table
	When I search in the table by AutoTest position 2 dropdown option in StartPosition dropdown
	Then assert that row with Regular is visible in the table
	And assert that row with Fast is not visible in the table
	When I search in the table by All dropdown option in StartPosition dropdown
	Then assert that row with Regular is visible in the table
	And assert that row with Fast is visible in the table
	When I search in the table by AutoTest position 1 dropdown option in EndPosition dropdown
	Then assert that row with Regular is visible in the table
	And assert that row with Fast is not visible in the table
	When I search in the table by All dropdown option in EndPosition dropdown
	Then assert that row with Regular is visible in the table
	And assert that row with Fast is visible in the table
	When I search in the table by 4 search term in the BeatsCount search field
	Then assert that row with Regular is not visible in the table
	And assert that row with Fast is visible in the table
	When I clear search field BeatsCount
	Then assert that row with Regular is visible in the table
	And assert that row with Fast is visible in the table
	When I search in the table by Fast dropdown option in DynamicsType dropdown
	Then assert that row with Regular is not visible in the table
	And assert that row with Fast is visible in the table
	When I search in the table by All dropdown option in DynamicsType dropdown
	Then assert that row with Regular is visible in the table
	And assert that row with Fast is visible in the table

@positive
Scenario: Create figure option
	Given I search in the table by Third figure search term in the SearchTerm search field
	And I click the Options button
	And I click add button
	When I fill the figure option form with start position AutoTest position 1, end position AutoTest position 2, beats count 10, dynamics type Slow
	And I click the Save button
	Then assert that I see toaster message with text The figure option has been added successfully.
	And I have asserted that a figure option for figure with name Third figure, that has 10 beats counts, that has start position AutoTest position 1, that has end position AutoTest position 2 and has dynamics type Slow exists

@negative
Scenario: Create figure option with invalid data
	Given I search in the table by Third figure search term in the SearchTerm search field
	And I click the Options button
	And I click add button
	When I fill the BeatCounts field with 7
	And I click the Save button
	Then assert that I see validation error message for BeatCounts field with text The beats count field must be even.
	When I fill the BeatCounts field with 0
	And I click the Save button
	Then assert that I see validation error message for BeatCounts field with text The beats count field should be at least 2.
	When I fill the BeatCounts field with 62