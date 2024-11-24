using System.Data;

namespace CountryLocalDbWork
{
    public class Country
    {
        //id (int), fullName (string), shortName (string), alpha2Code (unique string)
        public int Id { get; set; }
        public string fullName { get; set; } = string.Empty;
        public string shortName { get; set; } = string.Empty;
        public string alpha2Code { get; set; } = string.Empty;

        public Country() { }
    }
}
