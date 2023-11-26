using System.Collections.Generic;

namespace Geolocation.Domain
{
    public class Location : EntityBase<int>
    {
        public int GeonameId { get; private set; }
        public string CountryFlag { get; private  set; }
        public string CallingCode { get; private set; }
        public bool IsEurope { get; private set; }
        public List<Language> Languages { get; private set; }

        public Location(
            int geonameId,
            string countryFlag, 
            string callingCode,
            bool isEurope,
            List<Language> languages) : base()
        { 
            GeonameId = geonameId;
            CountryFlag = countryFlag;
            CallingCode = callingCode;
            IsEurope = isEurope;
            Languages = languages;
        }

        public Location() { }
    }
}
