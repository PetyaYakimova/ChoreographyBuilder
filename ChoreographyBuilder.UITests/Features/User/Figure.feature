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
	And assert that row with Fourth figure is not visible in the table
	When I search in the table by First figure search term in the SearchTerm search field
	Then assert that the table has at least 1 rows
	And assert that row with First figure is visible in the table
	And assert that row with Second figure is not visible in the table
	When I clear search field SearchTerm
	Then assert that row with First figure is visible in the table
	And assert that row with Second figure is visible in the table
	# add searching by the dropdowns for start and end position and then by beats count and the dropdown for dynamics type
