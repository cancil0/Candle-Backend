using Candle.Model.Enums.EnumExtensions.Attributes;
using System.ComponentModel;

namespace Candle.Model.Enums
{
    public enum IsActiveKey
    {
        [Description("Video"), Value("0")]
        NotActive,

        [Description("Video"), Value("1")]
        Active
    }
}
