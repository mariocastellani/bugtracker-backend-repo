namespace Domain.Entities.Issues
{
    public enum IssueType
    {
        None = 0,

        [Description("Epic")]
        Epic = 1,

        [Description("User Story")]
        UserStory = 2,

        [Description("Feature")]
        Feature = 3,

        [Description("Improvement")]
        Improvement = 4,

        [Description("Bug")]
        Bug = 5
    }
}