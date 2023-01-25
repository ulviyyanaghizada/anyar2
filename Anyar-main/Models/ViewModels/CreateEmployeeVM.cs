namespace Exam2.Models.ViewModels
{
    public class CreateEmployeeVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public IFormFile ImageUrl { get; set; }
        public int ProfessionId { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
    }
}
