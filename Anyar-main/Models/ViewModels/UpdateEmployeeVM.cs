namespace Exam2.Models.ViewModels
{
    public class UpdateEmployeeVM
    {
        public string Name { get; set; }
        public string DEscription { get; set; }
        public IFormFile? Image { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Linkedin { get; set; }
        public int ProfessionId { get; set; }
        public Profession? Profession { get; set; }
    }
}
