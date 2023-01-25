using Exam2.DAL;
using Exam2.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace Exam2.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM home = new HomeVM { Employees = _context.Employees , Professions = _context.Professions};
            return View(home);

        }
    }
}
