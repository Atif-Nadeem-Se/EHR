using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EHR.Models;

namespace EHR.Controllers
{
    public class DiagnosisController : Controller
    {
        private readonly EHRDBContext _dbContext;

        public DiagnosisController(EHRDBContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            var eHRDBContext = _dbContext.Diagnosis.Include(d => d.Disease).Include(d => d.Patient);
            return View(await eHRDBContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosis = await _dbContext.Diagnosis
                .Include(d => d.Disease)
                .Include(d => d.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            return View(diagnosis);
        }

        public IActionResult Create()
        {
            ViewData["DiseaseId"] = new SelectList(_dbContext.Diseases, "Id", "Name");
            ViewData["PatientId"] = new SelectList(_dbContext.Patients, "Id", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,DiseaseId")] Diagnosis diagnosis)
        {

            _dbContext.Add(diagnosis);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosis = await _dbContext.Diagnosis.FindAsync(id);
            if (diagnosis == null)
            {
                return NotFound();
            }
            ViewData["DiseaseId"] = new SelectList(_dbContext.Diseases, "Id", "Name", diagnosis.DiseaseId);
            ViewData["PatientId"] = new SelectList(_dbContext.Patients, "Id", "FirstName", diagnosis.PatientId);
            return View(diagnosis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientId,DiseaseId")] Diagnosis diagnosis)
        {
            if (id != diagnosis.Id)
            {
                return NotFound();
            }


            try
            {
                _dbContext.Update(diagnosis);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiagnosisExists(diagnosis.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosis = await _dbContext.Diagnosis
                .Include(d => d.Disease)
                .Include(d => d.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            return View(diagnosis);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diagnosis = await _dbContext.Diagnosis.FindAsync(id);
            if (diagnosis != null)
            {
                _dbContext.Diagnosis.Remove(diagnosis);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosisExists(int id)
        {
            return _dbContext.Diagnosis.Any(e => e.Id == id);
        }
    }
}
