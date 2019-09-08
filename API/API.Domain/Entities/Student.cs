namespace API.Domain.Entities
{
    public class Student : Person
    {
        public short Age { get; set; }

        public virtual Class AssociatedClass { get; set; }
    }
}
