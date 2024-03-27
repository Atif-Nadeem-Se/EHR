using EHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EHR.Controllers
{
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        private readonly EHRDBContext _dbContext;

        public PatientController(ILogger<PatientController> logger, EHRDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var list = _dbContext.Patients.ToList();
            return View(list);
        }
        public async Task<IActionResult> Create(int?id)
        {
            if (id == null)
            {
                return View(new Patient());
            }
            var patient = await _dbContext.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (patient.Id == 0)
                    {
                        _dbContext.Patients.Add(patient);
                        _dbContext.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _dbContext.Patients.Update(patient);
                        _dbContext.SaveChanges(); 
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(patient);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _dbContext.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }
        public async Task<IActionResult> FetchForDisplay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosis = await _dbContext.Diagnosis.Where(m => m.PatientId == id)
                .Include(d => d.Disease)
                .Include(d => d.Patient)
                .ToListAsync();
            if (diagnosis == null)
            {
                return NotFound();
            }

            return PartialView("_PartialPatient", diagnosis);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _dbContext.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
