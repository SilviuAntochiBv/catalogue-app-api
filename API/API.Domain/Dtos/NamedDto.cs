namespace API.Domain.Dtos
{
    public abstract class NamedDto<TKey> : BaseDto<TKey>
    {
        public string Name { get; set; }
    }
}
