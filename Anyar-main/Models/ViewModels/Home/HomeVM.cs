using Microsoft.Extensions.Hosting;

namespace Exam2.Models.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Profession> Professions { get; set; }
    }
}
