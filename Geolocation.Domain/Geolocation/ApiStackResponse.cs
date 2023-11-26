using System.Collections.Generic;

namespace Geolocation.Domain
{
    public class ApiStackResponse
    {
        public string ip { get; set; }
        public string type { get; set; }
        public string continent_code { get; set; }
        public string continent_name { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string region_code { get; set; }
        public string? region_name { get; set; }
        public string? city { get; set; }
        public string? zip { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public ApiStackLocation location { get; set; }
    }

    public class ApiStackLanguage
    {
        public string code { get; set; }
        public string name { get; set; }
        public string native { get; set; }
    }

    public class ApiStackLocation
    {
        public int? geoname_id { get; set; }
        public string capital { get; set; }
        public List<ApiStackLanguage> languages { get; set; }
        public string country_flag { get; set; }
        public string country_flag_emoji { get; set; }
        public string country_flag_emoji_unicode { get; set; }
        public string calling_code { get; set; }
        public bool is_eu { get; set; }
    }
}
