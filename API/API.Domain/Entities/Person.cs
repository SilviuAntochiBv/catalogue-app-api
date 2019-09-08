namespace API.Domain.Entities
{
    public abstract class Person : BaseEntity<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
