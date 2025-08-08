Feature: UserStats
User can see stats for their own data and navigate from the page

Background:
	Given I log in as FirstUser
	And I open the Home/Stats page

@positive
Scenario: Check stats on the stats page
	Then assert that I see at least 4 for the My Figures label
	And assert that I see at least 3 for the My Verse Choreographies label
	And assert that I see at least 1 for the My Full Choreographies label

@positive
Scenario: Navigate from the stats page 
	When I click the See all positions link
	Then assert that I am on Admin/Position/All page
	When I open the Admin/Home/Stats page
	And I click the See all verse types link
	Then assert that I am on Admin/VerseType/All page
