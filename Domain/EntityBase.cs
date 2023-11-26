namespace GeolocationAPI.Domain
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; private set; }
    }   
}
