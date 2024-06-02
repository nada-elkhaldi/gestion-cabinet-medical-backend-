using App_GCM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_GCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly DB_GCMContext _reactContext;
        public PatientsController(DB_GCMContext reactContext)
        {
            _reactContext = reactContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var patients = await _reactContext.Patients.ToListAsync();
            return Ok(patients);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Patient newPatient)
        {
            _reactContext.Patients.Add(newPatient);
            await _reactContext.SaveChangesAsync();
            return Ok(newPatient);

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var patientById = await _reactContext.Patients.FindAsync(id);
            return Ok(patientById);

        }
        [HttpPut]
        public async Task<IActionResult> Put(Patient patientToUpdate)
        {
            _reactContext.Patients.Update(patientToUpdate);
            await _reactContext.SaveChangesAsync();
            return Ok(patientToUpdate);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var PatientToDelete = await _reactContext.Patients.FindAsync(id);
            if (PatientToDelete == null)
            {
                return NotFound();
            }
            _reactContext.Patients.Remove(PatientToDelete);
            await _reactContext.SaveChangesAsync();
            return Ok();

        }

    }
}
