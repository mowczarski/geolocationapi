using System.Collections.Generic;

namespace GeolocationAPI.Domain
{
    public class Location : EntityBase<int>
    {
        public int GeolocationId { get; set; }
        public string CountryFlag { get; set; }
        public string CallingCode { get; set; }
        public bool IsEurope { get; set; }
        public List<Language> Languages { get; set; }
    }
}
