using System.Collections.Generic;

namespace Candle.Model.Entities
{
    public class ProfileStatusDef
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public ICollection<User> User { get; set; }
    }
}
