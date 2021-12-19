using Candle.Model.Enums.EnumExtensions.Attributes;
using System.ComponentModel;

namespace Candle.Model.Enums
{
    public enum ProfileStatusKey
    {
        [Description("Confidential"), Value("C")]
        Confidential,

        [Description("Follower"), Value("F")]
        Follower,

        [Description("Everyone"), Value("E")]
        Everyone
    }

}
