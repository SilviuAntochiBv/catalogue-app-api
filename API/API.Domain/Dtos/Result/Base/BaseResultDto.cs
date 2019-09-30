namespace API.Domain.Dtos.Result.Base
{
    public abstract class BaseResultDto<TKey>
    {
        public TKey Id { get; set; }
    }
}
