namespace Geolocation.Domain
{
    public class Language : EntityBase<int>
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Native { get; private set; }

        public Language(string code, string name, string native) 
        {
            Code = code;
            Name = name;
            Native = native;
        }

        public Language() { }
    }
}
