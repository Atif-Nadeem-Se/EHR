using EHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EHR.Controllers
{
    public class DoctorController : Controller
    {
        private readonly EHRDBContext _dbContext;
        public DoctorController(EHRDBContext context)
        {
            _dbContext = context;
        }
        public async Task<IActionResult> Index()
        {
            var doctors = await _dbContext.Doctors.ToListAsync();
            return View(doctors);

        }
        public IActionResult Create()
        {
            return View(new Doctor());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {

            _dbContext.Add(doctor);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _dbContext.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Doctor diagnosis)
        {
            try
            {
                _dbContext.Update(diagnosis);
                await _dbContext.SaveChangesAsync();
                return Ok("updated");
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw;

            }

        }
    }
}
