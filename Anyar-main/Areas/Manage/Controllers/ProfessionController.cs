using Exam2.DAL;
using Exam2.Models;
using Exam2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace Exam2.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles="Admin")]
    public class ProfessionController : Controller
    {
        readonly AppDbContext _context;
        public ProfessionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Professions.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateProfessionVM professionVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Profession profession = new Profession { Name=professionVM.Name };
            _context.Professions.Add(profession);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete (int? id)
        {
            if (id is null || id<=0)
            {
                return BadRequest();
            }
            Profession profession = _context.Professions.FirstOrDefault(p=>p.Id==id);
            if (profession is null)
            {
                return BadRequest();
            }
            _context.Professions.Remove(profession);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }

        public IActionResult Update(int? id)
        {
            if(id is null) return BadRequest();
            Profession profession = _context.Professions.FirstOrDefault(p=>p.Id==id);
            if (profession is null) return NotFound();
            CreateProfessionVM professionVM = new CreateProfessionVM { Name =profession.Name};
            return View(professionVM);
        }
        [HttpPost]
        public IActionResult Update(int? id, CreateProfessionVM professionVM)
        {
            if (!ModelState.IsValid) return View();
            if (id is null) return BadRequest();
            Profession exist = _context.Professions.FirstOrDefault(p => p.Id == id);
            if (exist is null) return NotFound();
            exist.Name=professionVM.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
