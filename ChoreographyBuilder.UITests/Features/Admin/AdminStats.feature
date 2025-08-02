Feature: AdminStats
Admin user can see stats for the data in the system and navigate from the page

Background:
	Given I log in as AdminUser
	And I open the Admin/Home/Stats page

@positive
Scenario: Check stats on the stats page
	Then assert that I see at least 5 for the All Positions label
	And assert that I see at least 4 for the Active Positions label
	And assert that I see at least 5 for the All Verse Types label
	And assert that I see at least 4 for the Active Verse Types label
	And assert that I see at least 5 for the All Figures label
	And assert that I see at least 2 for the Users With Figures label
	And assert that I see at least 4 for the All Verse Choreographies label
	And assert that I see at least 2 for the Users With Verse Choreographies label
	And assert that I see at least 2 for the All Full Choreographies label

@positive
Scenario: Navigate from the stats page 
	Given [context]
	When [action]
	Then [outcome]
