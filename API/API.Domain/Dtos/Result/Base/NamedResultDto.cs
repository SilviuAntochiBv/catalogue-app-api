namespace API.Domain.Dtos.Result.Base
{
    public abstract class NamedResultDto<TKey> : BaseResultDto<TKey>
    {
        public string Name { get; set; }
    }
}
