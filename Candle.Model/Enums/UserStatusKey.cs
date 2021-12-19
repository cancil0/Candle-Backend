using Candle.Model.Enums.EnumExtensions.Attributes;
using System.ComponentModel;

namespace Candle.Model.Enums
{
    public enum UserStatusKey
    {
        [Description("NotActive"), Value("0")]
        NotActive,

        [Description("NeedConfirm"), Value("1")]
        NeedConfirm,

        [Description("Active"), Value("2")]
        Active
    }
}
