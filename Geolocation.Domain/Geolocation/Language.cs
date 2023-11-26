namespace Geolocation.Domain
{
    public class Language : EntityBase<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Native { get; set; }
    }
}
