namespace API.Domain.Dtos.Result.Base
{
    public abstract class PersonResultDto<TKey> : BaseResultDto<TKey>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
