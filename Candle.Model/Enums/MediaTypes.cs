using Candle.Model.Enums.EnumExtensions.Attributes;
using System.ComponentModel;

namespace Candle.Model.Enums
{
    public enum MediaTypes
    {
        [Description("Photo"), Value("1")]
        Photo,

        [Description("Video"), Value("2")]
        Video
    }
}
