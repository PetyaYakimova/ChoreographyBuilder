Feature: Navigation
All kinds of users can navigate in the app.

@positive
Scenario: Admin user can navigate through the menus
	When I log in as AdminUser
	Then assert that the menus I see in the header are Stats, Users, Positions, Verse Types
	And assert that I am on Admin/Home/Stats page
	When I click on Users in the header navigation
	Then assert that I am on Admin/User/All page
	When I click on Stats in the header navigation
	Then assert that I am on Admin/Home/Stats page
	When I click on Positions in the header navigation
	Then assert that I am on Admin/Position/All page
	When I click on Verse Types in the header navigation
	Then assert that I am on Admin/VerseType/All page
	# Add step to click on the logo and assert that we are on the home page

@positive
Scenario: User can navigate through the menus
	When I log in as FirstUser
	Then assert that the menus I see in the header are Stats, My Figures, My Saved Verse Choreographies, My Saved Full Choreographies
	And assert that I am on Home/Stats page
	When I click on My Figures in the header navigation
	Then assert that I am on Figure/Mine page
	When I click on Stats in the header navigation
	Then assert that I am on Home/Stats page
	When I click on My Saved Verse Choreographies in the header navigation
	Then assert that I am on VerseChoreography/Mine page
	When I click on My Saved Full Choreographies in the header navigation
	Then assert that I am on FullChoreography/Mine page
	# Add step to click on the logo and assert that we are on the home page
