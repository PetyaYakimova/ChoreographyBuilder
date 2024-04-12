# ChoreographyBuilder

ChoreographyBuilder is made to assist dancers in building choreographies. More specifically it is build to suit the needs of swing dancers for now.

There are two user roles in it - **Administrator** and **User**. When self-registering you can only create a User profile.

## The Administrator
If you have seeded the DB with the data from the repo, you can use:
> Email: admin@mail.com
> Password: admin123

The Administrator sees statistics about the number of positions, verse types, figures, verse choreographies and full choreographies. They also see statistics for each user - number of figures, verse choreographies and full choreorgaphies.

The Administrator can do all CRUD operations for the nomenclatures in the system - the **positions** and the **verse types**. The same positions and verse types are used by all users in the system. If a position or verse type is used already for a figure option or verse choreography it cannot be deleted or edited. 
The status of every position or verse type can be changed at any time between active and inactive, and while this will not affect the already existing records that have a foreign key to them, an inactive position or verse type can no longer be selected when creating a new figure option or verse choreography.
There is added custom validation that the number of beats for a verse type must be even number.

Administrator can never access the pages made for Users, they can never use the functionalities for creating figures with options, verse choreographies or full choreorgpahies.

## The User
If you have seeded the DB with the data from the repo, you can use:
> Email: demo@mail.com
> Password: demo123

The User always sees **only their own data**, they can never access the data of other users. They can never access the pages made for Administrator - the nomenclatures (positions and verse types) and system and user statistics.

In order to build choreogrpahies, the users must enter their **figures with options**. They are the main building blocks and most records should exists here. The figure options represent the different ways a certain figure can be performed, for example with different start position or for a different number of beats and with different dynamics type. 
For the figure options again there is a custom validation that the number of beats is even, since it is made to suit the needs of swing dancers. Also, an enum for dynamis type is used. 
The figure itself can be marked as a favourite figure and as a highlight figure. The highlight figures are those that are more interesting and are usually used to end a verse with them. All figures that are not highlights are considered figures that can be used in the start ot middle of the verse.
Users can do all CRUD operations for figures and figure options. Only delete and edit are limited to only records that are not used in verse choreographies.

The **verse choreorgaphies** are the most interesting part of the app and its main reason to exist. A user must select a verse type for the verse choreography and an ending highligh figure, and optionally a start position for the verse. Then the app generates all possible options for consequitive figures that fills those requirements and scores each of them based on selected different dynamics types, used favourite figures, etc. Then only the top suggestions are left and only they are displayed for the user. 
The user sees this list of suggestions with their scores and decides which verse choreography best suits their needs. When they select it - they must fill a name and save it.
A User can see a list of all they saved verse choreographies with details and can delete only those verse choreographies that are not used in full choreographies.

A **full choreography** is made up of verse choreographies in a specific order. A User can create a new full choreography and then add one by one its verse choreographies. For example when building a full choreography for the song Roll Over Bethoven, the User will select firstly a verse choreography that is for a swing verse, then for a second verse choreorgaphy he can only select of the verse choreorgaphies that start with the same position that the previous verse has ended with, etc. 
So in order to keep the correct order and the correct start-end position between every verse choreorgaphy - only the last verse choreorgaphy from the list in the full choreorgaphy can be deleted.
The user can also delete a full choreography or edit its name at any time.
