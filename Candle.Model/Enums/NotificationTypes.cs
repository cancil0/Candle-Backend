using Candle.Model.Enums.EnumExtensions.Attributes;
using System.ComponentModel;

namespace Candle.Model.Enums
{
    public enum NotificationTypes
    {
        [Description("FollowingNotification"), Value("FN")]
        FollowingNotification,

        [Description("PostNotification"), Value("PN")]
        PostNotification
    }
}
