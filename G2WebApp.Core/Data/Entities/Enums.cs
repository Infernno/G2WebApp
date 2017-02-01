using System.ComponentModel;

namespace G2WebApp.Core.Data.Entities
{
    public enum ContentType
    {
        Text = 0,
        Image = 1,
        Video = 2
    }

    public enum FlagType
    {
        IsWaiting = 0,
        Approved = 1,
        Delete = 2,
        Spam = 3
    }

    public enum UserGroup
    {
        [Description("Administrator")]
        Administrator,

        [Description("Moderator")]
        Moderator,

        [Description("User")]
        User,

        [Description("Banned")]
        Banned
    }

    public enum VoteType
    {
        Story = 0,
        Comment = 1
    }
}
