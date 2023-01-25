namespace Exam2.Models
{
    public class Employee:BaseNameEntity
    {
        public string Surname { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int? ProfessionId { get; set; }
        public Profession Profession { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
    }
}
