﻿namespace ChoreographyBuilder.Core.Constants;

public static class MessageConstants
{
    public const string RequiredErrorMessage = "The {0} field is required.";

    public const string StringLengthErrorMessage = "The {0} field must be between {2} and {1} characters long.";

    public const string NumberMustBeEvenErrorMessage = "The number must be even.";

    public const string NumberMustBeInRangeErrorMessage = "The number must be between {1} and {2}.";

    public const string PositionDoesntExistErrorMessage = "Position doesn't exist!";

    public const string DynamicsTypeDoesntExistErrorMessage = "Dynamics type doesn't exist!";

    public const string VerseChoreographyDoesntExistErrorMessage = "Verse choreography doesn't exist!";

    public const string InvalidVerseChoreographyOrderErrorMessage = "Verse choreography is with invalid order!";

    public const string VerseChoreorgaphyStartsWithWrongPositionErrorMessage = "The verse choreography starts with the wrong position to be included in this full choreography!";

    public const string EntityWithIdWasNotFoundLoggerErrorMessage = "{0} with id {1} was not found!";

    public const string BeatsCountIsNotEvenNumberLoggerErrorMessage = "The beats count for {0} is not an even number!";

    public const string UserForTheVerseChoreographyAndForTheFullChoreographyIsNotTheSameLoggerErrorMessage = "The user for the verse choreography and the user for the full choreography are not the same!";

    public const string UserForTheFigureAndForTheVerseChoreographyIsNotTheSameLoggerErrorMessage = "The user for the figure and the user for the verse choreography are not the same!";

    public const string UnmatchedFigureIdsLoggerErrorMessage = "Difference between the sent figureId and the figure id from the figure option.";

    public const string UserMessageSuccess = "UserMessageSuccess";

    public const string UserMessageError = "UserMessageError";

    public const string ChangedStatusSuccessMessage = "The status of the {0} has been successfully changed.";

    public const string ItemAddedSuccessMessage = "The {0} has been added successfully.";

    public const string ItemUpdatedSuccessMessage = "The {0} has been updated successfully.";

    public const string ItemDeletedSuccessMessage = "The {0} has been deleted.";

    public const string InvalidRequestForGeneratingVerseChoreographiesErrorMessage = "Invalid request for generating verse choreographies!";

    public const string InvalidVerseTypeIdWhenSavingVerseChoreographyErrorMessage = "Invalid verse type id when saving verse choreography!";

    public const string InvalidFigureOptionIdWhenSavingVerseChoreographyErrorMessage = "Invalid figure option id when saving verse choreography!";

    public const string InvalidFigureOrderWhenUpdatingVerseChoreographyErrorMessage = "Invalid figure order when updating verse choreography!";

    public const string NewFigureDoesNotMatchOldFigureDataErrorMessage = "The new figure has different start position, end position or beats count!";

    public const string VerseChoreographiesSuggestionsGeneratedSuccessMessage = "Verse choreographies suggestions are generated.";

    public const string InvalidVerseChoreographyErrorMessage = "This verse choreography is not valid!";

    public const string FigureOptionDoesntExistErrorMessage = "Figure option doesn't exist!";

    public const string InvalidFigureOrderErrorMessage = "Figure is with invalid order!";

    public const string FigureHasTooManyBeatsErrorMessage = "Figure has too many beats to be included in this choreography!";

    public const string FigureStartsWithWrongPositionErrorMessage = "Figure starts with the wrong position to be included in this choreography!";

    public const string PositionAsString = "position";

    public const string VerseTypeAsString = "verse type";

    public const string FigureAsString = "figure";

    public const string FigureOptionAsString = "figure option";

    public const string VerseChoreographyAsString = "verse choreography";

    public const string FullChoreographyAsString = "full choreography";
}
