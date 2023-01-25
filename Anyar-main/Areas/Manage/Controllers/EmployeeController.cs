using Exam2.DAL;
using Exam2.Models;
using Exam2.Models.ViewModels;
using Exam2.Utilities.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Exam2.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Employees.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Professions = new SelectList(_context.Professions,nameof(Profession.Id),nameof(Profession.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeVM employeeVM)
        {
            string result = employeeVM.ImageUrl.CheckValidate("imgae/", 800);
            if (result.Length > 0)
            {
                ModelState.AddModelError("image", result);

            }
            if (!_context.Professions.Any(p => p.Id == employeeVM.ProfessionId))
            {
                ModelState.AddModelError("ProfessionId", "bele bir profession yoxdur");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Professions = new SelectList(_context.Professions, nameof(Profession.Id), nameof(Profession.Name));
                return View();
            }
            Employee employee = new Employee
            {
                Name = employeeVM.Name,
                ProfessionId = employeeVM.ProfessionId,
                Facebook = employeeVM.Facebook,
                Twitter = employeeVM.Twitter,
                Instagram = employeeVM.Instagram,
                Description = employeeVM.Description,
                ImageUrl =employeeVM.ImageUrl.SaveFile(Path.Combine(_env.WebRootPath,"assets","img"))
            };
            _context.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) { return BadRequest(); }
            Employee employee = await _context.Employees.FirstOrDefaultAsync(p => p.Id == id);
            if (employee == null) { return NotFound(); }
            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM
            {
                Name = employee.Name,
                ProfessionId = (int)employee.ProfessionId,
                DEscription = employee.Description,
                Facebook = employee.Facebook,
                Twitter = employee.Twitter,
                Instagram = employee.Instagram,
                Linkedin = employee.Linkedin,
            };
            ViewBag.Professions = new SelectList(_context.Professions, nameof(Profession.Id), nameof(Profession.Name));
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateEmployeeVM employeeVM)
        {
            if (id == null) { return BadRequest(); }
            var existedemployee = await _context.Employees.FirstOrDefaultAsync(p => p.Id == id);
            if (existedemployee == null) { return NotFound(); }
            if (!_context.Professions.Any(p => p.Id == employeeVM.ProfessionId)) ModelState.AddModelError("PositionId", "bele bir position yoxdu");
            if (employeeVM.Image != null)
            {
                string result = employeeVM.Image.CheckValidate("image/", 800);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }
                else
                {
                    existedemployee.ImageUrl.DeleteFile(_env.WebRootPath, "assets/img");
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Professions = new SelectList(_context.Professions, nameof(Profession.Id), nameof(Profession.Name));
                return View();
            }
            existedemployee.Name = employeeVM.Name;
            existedemployee.ProfessionId = employeeVM.ProfessionId;
            existedemployee.Description = employeeVM.DEscription;
            existedemployee.Facebook = employeeVM.Facebook;
            existedemployee.Twitter = employeeVM.Twitter;
            existedemployee.Instagram = employeeVM.Instagram;
            existedemployee.Linkedin = employeeVM.Linkedin;
            if (employeeVM.Image != null)
            {
                existedemployee.ImageUrl = employeeVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img"));
            }
            ViewBag.Professions = new SelectList(_context.Professions, nameof(Profession.Id), nameof(Profession.Name));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
