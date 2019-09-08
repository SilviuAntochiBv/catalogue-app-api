namespace API.Domain.Dtos
{
    public abstract class BaseDto<TKey>
    {
        public TKey Id { get; set; }
    }
}
