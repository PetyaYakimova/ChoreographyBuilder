Feature: UserStats
User can see stats for their own data and navigate from the page

Background:
	Given I log in as FirstUser
	And I open the Home/Stats page

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
	And assert that I see at least 2 for the Users With Full Choreographies label

@positive
Scenario: Navigate from the stats page 
	When I click the See all positions link
	Then assert that I am on Admin/Position/All page
	When I open the Admin/Home/Stats page
	And I click the See all verse types link
	Then assert that I am on Admin/VerseType/All page
