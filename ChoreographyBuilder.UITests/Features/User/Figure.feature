Feature: Figure
User can view, create, edit, copy and delete figures and their options

Background:
	Given I log in as FirstUser
	And I open the Figure/Mine page

@positive
Scenario: View figure table and search in it
	Then assert that the table has at least 2 rows
	And assert that the table has columns with names Name, Number of options, Highlight, Favourite, Shared
