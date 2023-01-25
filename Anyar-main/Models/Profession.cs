namespace Exam2.Models
{
    public class Profession:BaseNameEntity
    {
        public ICollection<Employee>? Employees { get; set; }
    }
}
