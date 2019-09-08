namespace API.Domain.Dtos
{
    public abstract class PersonDto<TKey> : BaseDto<TKey>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
