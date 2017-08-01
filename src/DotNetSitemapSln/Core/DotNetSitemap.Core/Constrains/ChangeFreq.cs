
namespace DotNetSitemap.Core.Constrains
{
    public sealed class ChangeFreq
    {
        private string _value;
        private ChangeFreq(string value)
        {
            _value = value;
        }
        public override string ToString()
        {
            return _value;
        }
        public string Value => _value;
        public static readonly ChangeFreq Always = new ChangeFreq("always");
        public static readonly ChangeFreq Hourly = new ChangeFreq("hourly");
        public static readonly ChangeFreq Daily = new ChangeFreq("daily");
        public static readonly ChangeFreq Weekly = new ChangeFreq("weekly");
        public static readonly ChangeFreq Monthly = new ChangeFreq("monthly");
        public static readonly ChangeFreq Yearly = new ChangeFreq("yearly");
        public static readonly ChangeFreq Never = new ChangeFreq("never");
    }
}
